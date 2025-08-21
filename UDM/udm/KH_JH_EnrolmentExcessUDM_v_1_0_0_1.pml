<Project Name="KH_JH_EnrolmentExcessUDM">
	<Property Name="UDT">
		<Release Version="1.0.0.0" URL="udm/UDT_1_0_0_0.tcmd" />
		<Release Version="1.0.0.1" URL="udm/UDT_1_0_0_1.tcmd"></Release>
	</Property>
	<Property Name="UDS">
		<Contract Name="kh.EnrolmentExcess.student" Enabled="True">
	<Definition>
		<Authentication Extends="auth.student" />
	</Definition>
	<Package Name="_">
		<Service Enabled="true" Name="GetCredits">
			<Definition Type="dbhelper">
				<Action>Select</Action>
				<SQLTemplate><![CDATA[SELECT @@FieldList FROM $kh.enrolment_excess.credits WHERE @@Condition]]></SQLTemplate>
				<ResponseRecordElement>Credits</ResponseRecordElement>
				<FieldList Name="FieldList" Source="Field">
					<Field Alias="Balanced" Mandatory="True" Source="Balanced" Target="balanced" />
					<Field Alias="Competition" Mandatory="True" Source="Competition" Target="competition" />
					<Field Alias="Fitness" Mandatory="True" Source="Fitness" Target="fitness" />
					<Field Alias="Merit" Mandatory="True" Source="Merit" Target="merit" />
					<Field Alias="Services" Mandatory="True" Source="Services" Target="services" />
					<Field Alias="Term" Mandatory="True" Source="Term" Target="term" />
					<Field Alias="Verification" Mandatory="True" Source="Verification" Target="verification" />
					<Field Alias="Detail" Mandatory="True" OutputType="xml" Source="Detail" Target="detail" />
				</FieldList>
				<Conditions Name="Condition" Required="False" Source="Condition">
					<Condition Required="True" Source="MyStudentId" SourceType="Variable" Target="ref_student_id" />
				</Conditions>
				<InternalVariable>
					<Variable Key="StudentID" Name="MyStudentId" Source="UserInfo" />
				</InternalVariable>
			</Definition>
		</Service>
		<Service Enabled="true" Name="GetNow">
			<Definition Type="Javascript">
				<Code><![CDATA[
			var pattern = getRequest().Pattern || 'yyyy/MM/dd HH:mm:ss';
			var dateStr = new Date().format(pattern);
			return {DateTime: dateStr};
		]]></Code>
			</Definition>
		</Service>
		<Service Enabled="true" Name="GetOpening">
			<Definition Type="dbhelper">
				<Action>Select</Action>
				<SQLTemplate><![CDATA[SELECT @@FieldList FROM $kh.enrolment_excess.input_date WHERE @@Condition @@Order]]></SQLTemplate>
				<ResponseRecordElement>Opening</ResponseRecordElement>
				<FieldList Name="FieldList" Source="Field">
					<Field Alias="EndDate" Mandatory="True" OutputConverter="DateTime" Source="EndDate" Target="end_date" />
					<Field Alias="StartDate" Mandatory="True" OutputConverter="DateTime" Source="StartDate" Target="start_date" />
				</FieldList>
				<Conditions Name="Condition" Required="False" Source="" />
				<Orders Name="Order" Source="Order" />
				<Pagination Allow="True" />
			</Definition>
		</Service>
		<Service Enabled="true" Name="UpdateCredits">
			<Definition Type="dbhelper">
				<Action>Update</Action>
				<SQLTemplate><![CDATA[UPDATE $kh.enrolment_excess.credits SET @@FieldList  WHERE @@Condition]]></SQLTemplate>
				<RequestRecordElement>Credits</RequestRecordElement>
				<FieldList Name="FieldList" Source="">
					<Field Source="LastUpdate" SourceType="Variable" Target="last_update" />
					<Field Source="Balanced" Target="balanced" />
					<Field Source="Competition" Target="competition" />
					<Field Source="Fitness" Target="fitness" />
					<Field Source="Merit" Target="merit" />
					<Field Source="Services" Target="services" />
					<Field Source="Term" Target="term" />
					<Field Source="Verification" Target="verification" />
					<Field InputType="xml" Source="Detail" Target="detail" />
				</FieldList>
				<Conditions Name="Condition" Required="False" Source="Condition">
					<Condition Required="True" Source="MyStudentId" SourceType="Variable" Target="ref_student_id" />
				</Conditions>
				<InternalVariable>
					<Variable Key="StudentID" Name="MyStudentId" Source="UserInfo" />
					<Variable Name="LastUpdate" Source="Literal">now()</Variable>
				</InternalVariable>
				<Preprocesses>
					<Preprocess InvalidMessage="502" Name="validDate" Type="validate"><![CDATA[
			select count(*)>0 from $kh.enrolment_excess.input_date 
			where start_date<=Now() and end_date>=Now()
			]]></Preprocess>
					<Preprocess Name="NotExists" Type="update"><![CDATA[
			INSERT INTO $kh.enrolment_excess.credits (ref_student_id)
			SELECT '@@MyStudentId'
			WHERE NOT EXISTS (SELECT 1 FROM $kh.enrolment_excess.credits WHERE ref_student_id='@@MyStudentId');
			]]></Preprocess>
				</Preprocesses>
			</Definition>
		</Service>
	</Package>
</Contract>
	</Property>
</Project>