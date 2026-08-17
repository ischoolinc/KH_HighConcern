<Project Name="KHCentralOffice">
	<Property Name="UDT">
		<Release Version="1.0.0.10" URL="udm/UDT_1_0_0_10.tcmd" />
		<Release Version="1.0.0.11" URL="udm/UDT_1_0_0_11.tcmd"></Release>
	</Property>
	<Property Name="UDS">
		<Contract Name="ischool.kh.CentralOffice" Enabled="True">
	<Definition>
		<Authentication>
			<Passport Enabled="true">
				<IssuerList>
					<Issuer Name="greening.shared.user">
						<CertificateProvider Type="HttpGet">https://auth.ischool.com.tw:8443/dsa/greening/info/Public.GetPublicKey</CertificateProvider>
						<AccountLinking Type="mapping">
							<TableName>$kh_central.office_system.user</TableName>
							<UserNameField>userid</UserNameField>
							<MappingField>userid</MappingField>
							<Properties />
						</AccountLinking>
					</Issuer>
				</IssuerList>
			</Passport>
		</Authentication>
	</Definition>
	<Package Name="_">
		<Service Enabled="true" Name="GetSemester">
			<Definition Type="dbhelper">
				<Action>Select</Action>
				<SQLTemplate><![CDATA[SELECT @@FieldList 
FROM list 
WHERE name='系統設定']]></SQLTemplate>
				<ResponseRecordElement />
				<FieldList Name="FieldList" Source="Field">
					<Field Alias="Result" Mandatory="True" OutputType="Xml" Source="Content" Target="content" />
				</FieldList>
				<Pagination Allow="True" />
			</Definition>
		</Service>
		<Service Enabled="true" Name="GetStudents">
			<Definition Type="DBHelper">
	<Action>Select</Action>
	<SQLTemplate>
		<![CDATA[SELECT @@FieldList 
		from student left outer  join tag_student on student.id=tag_student.ref_student_id 
		left outer join tag on tag.id=tag_student.ref_tag_id 
		left outer join class on class.id=student.ref_class_id 
		left outer join $kh_central.office_system.student_category_mapping on  (CASE WHEN tag.prefix='' THEN tag.name ELSE tag.prefix || ':' || tag.name  END)=$kh_central.office_system.student_category_mapping.student_category 
		WHERE student.status=1 
	    ]]>
	</SQLTemplate>
	<ResponseRecordElement>Students</ResponseRecordElement>
	<FieldList Name="FieldList" Source="Field">
		<Field Alias="ID" Mandatory="False" Source="ID" Target="student.id" />
		<Field Alias="Name" Mandatory="True" Source="Name" Target="student.name" />
		<Field Alias="IdNumber" Mandatory="True" Source="IdNumber" Target="student.id_number" />
		<Field Alias="SeatNo" Mandatory="True" Source="SeatNo" Target="student.seat_no" />
		<Field Alias="StudentNumber" Mandatory="True" Source="StudentNumber" Target="student.student_number" />
		<Field Alias="Gender" Mandatory="True" OutputConverter="BitToGender" Source="Gender" Target="student.gender" />
		<Field Alias="ClassName" Mandatory="True" Source="ClassName" Target="class.class_name" />
		<Field Alias="GradeYear" Mandatory="True" Source="GradeYear" Target="class.grade_year" />
		<Field Alias="CentralCategory" Mandatory="True" OutputType="Attribute" Source="CentralCategory" Target="central_category" />
		<Field Alias="TagID" Mandatory="False" OutputType="Attribute" Source="TagID" Target="tag.id" />
		<Field Alias="TagName" Mandatory="True" OutputType="Attribute" Source="TagName" Target="(CASE WHEN tag.prefix='' THEN tag.name ELSE tag.prefix || ':' || tag.name  END)" />
	</FieldList>
	<ExportStyle>
		<Group Fields="ID,Name,IdNumber,SeatNo,StudentNumber,Gender,ClassName,GradeYear" Name="Student">
			<Group Name="Tags">
				<Record Name="Tag">
					<!--<Element Field="TagID"/>-->
					<Element Field="TagName" Identity="True" />
					<Element Field="CentralCategory" />
				</Record>
			</Group>
		</Group>
	</ExportStyle>
	<Conditions Name="Condition" Required="False" Source="Condition" />
	<Orders Name="Order" Source="Order" />
	<Pagination Allow="True" />
</Definition>
		</Service>
	</Package>
</Contract>
	</Property>
</Project>