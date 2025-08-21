<Project Name="KHCentralOffice">
	<Property Name="UDT">
		<Release Version="1.0.0.10" URL="udm/UDT_1_0_0_10.tcmd" />
		<Release Version="1.0.0.11" URL="udm/UDT_1_0_0_11.tcmd" />
		<Release Version="1.0.0.12" URL="udm/UDT_1_0_0_12.tcmd"></Release>
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
		<Service Enabled="true" Name="GetSchoolInfo">
			<Definition Type="dbhelper">
	<Action>Select</Action>
	<SQLTemplate>
		<![CDATA[SELECT @@FieldList FROM list WHERE name='學校資訊']]>
	</SQLTemplate>
	<ResponseRecordElement />
	<FieldList Name="FieldList" Source="Field">
		<Field Alias="Result" Mandatory="True" OutputType="Xml" Source="Content" Target="content" />
	</FieldList>
	<Pagination Allow="True" />
</Definition>
		</Service>
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
		<Service Enabled="true" Name="GetUnPass4Students">
			<Definition Type="javascript">
	<Code>// JavaScript source code


var _request = getRequest();

if (_request["Request"]) _request = _request["Request"];

var result = {};

// 各年級學生人數
var sql = getResource('sqlstudentcount');
// 處理傳入學年度學期
sql = sql.replace(/@@SchoolYear/g, _request["SchoolYear"]).replace(/@@Semester/g, _request["Semester"]);
//result.request=_request;
//result.sql=sql;
var gradeObj = {};
var rs = executeSql(sql);
while (rs.next()) {
    if (!result.GradeYear)
        result.GradeYear = [];
    var gobj = {
        "@Grade": rs.get("gradeyear"),
        "@Totle": rs.get("totle"),
        "@UnPassCount": 0
    };
    gradeObj[gobj["@Grade"]] = gobj;
    result.GradeYear.push(gobj);
}

// 處理各年級有4個領域不及格
sql = getResource('sqlunpassstudent');
sql = sql.replace(/@@SchoolYear/g, _request["SchoolYear"]).replace(/@@Semester/g, _request["Semester"]);
var gidObj = {};
var chk4Id = {};
var rsObjs = [];
var rs1 = executeSql(sql);

while (rs1.next()) {

    var gidKey = rs1.get("gradeyear") + "^^^^" + rs1.get("id");
    data = {
        key: gidKey,
        "id": rs1.get("id"),
        "gradeyear": rs1.get("gradeyear"),
        "class_name": rs1.get("class_name"),
        "seat_no": rs1.get("seat_no"),
        "student_number": rs1.get("student_number"),
        "name": rs1.get("name"),
        "domain": rs1.get("domain"),
        "score": rs1.get("score")
    }
    rsObjs.push(data);


    if (!chk4Id[gidKey]) { chk4Id[gidKey] = 0; }

    chk4Id[gidKey]++;
}

for (var i = 0; i &lt; rsObjs.length; i++) {
    var item = rsObjs[i];
    if (!result.GradeYear)
        result.GradeYear = [];
    // 使用年級+StudentID當key
    var gidKey = item.key;
    // 有四個領域不及格包含四個
    if (chk4Id[gidKey] &gt; 3) {
        if (!gidObj[gidKey]) {
            var gobj = gradeObj[item.gradeyear];
            var giobj = {
                "@ClassName": item.class_name,
                "@SeatNo": item.seat_no,
                "@StudentNumber": item.student_number,
                "@Name": item.name
            };
            gidObj[gidKey] = giobj;

            if (!gobj.UnPassStudent)
                gobj.UnPassStudent = [];
            gobj.UnPassStudent.push(giobj);
            gobj["@UnPassCount"]++;
        }
        if (!gidObj[gidKey].Domain)
            gidObj[gidKey].Domain = [];

        gidObj[gidKey].Domain.push({
            "@Domain": item.domain,
            "@Score": item.score
        });
    }

}

return result;

</Code>
	<Resources>
		<Resource Name="sqlstudentcount">
			<![CDATA[
SELECT gradeyear, count(distinct ref_student_id) as totle
FROM (
	SELECT 
		sems_subj_score.ref_student_id, sh.gradeyear, sems_subj_score.school_year, sems_subj_score.semester,domain, CAST(score as decimal) as score
	FROM 
		xpath_table( 
			'id'
			, '''<root>''||score_info||''</root>'''
			, 'sems_subj_score'
			, '/root/Domains/Domain/@領域|/root/Domains/Domain/@成績'
			, 'school_year = @@SchoolYear and semester = @@Semester'
		) AS s1 (id int, domain character varying, score character varying) 
		LEFT OUTER JOIN sems_subj_score on sems_subj_score.id = s1.id
		LEFT OUTER JOIN xpath_table( 
			'id'
			, '''<root>''||sems_history||''</root>'''
			, 'student'
			, '/root/History[ ( @SchoolYear=''@@SchoolYear'' ) and (@Semester=''@@Semester'')]/@GradeYear'
			, 'true'
		) AS sh(id int, gradeyear integer) on sh.id = sems_subj_score.ref_student_id
	WHERE 
		score <> '' and gradeyear is not null
) as student_domain_score_table
GROUP BY gradeyear
		]]>
		</Resource>
		<Resource Name="sqlunpassstudent">
			<![CDATA[
SELECT gradeyear, student.id, class.class_name, student.seat_no, student.student_number, student.name, domain, score
FROM 
	(
		SELECT 
			sems_subj_score.ref_student_id, sh.gradeyear, sems_subj_score.school_year, sems_subj_score.semester,domain, CAST(score as decimal) as score
		FROM 
			xpath_table( 
				'id'
				, '''<root>''||score_info||''</root>'''
				, 'sems_subj_score'
				, '/root/Domains/Domain/@領域|/root/Domains/Domain/@成績'
				, 'school_year = @@SchoolYear and semester = @@Semester'
			) AS s1 (id int, domain character varying, score character varying) 
			LEFT OUTER JOIN sems_subj_score on sems_subj_score.id = s1.id
			LEFT OUTER JOIN xpath_table( 
				'id'
				, '''<root>''||sems_history||''</root>'''
				, 'student'
				, '/root/History[ ( @SchoolYear=''@@SchoolYear'' ) and (@Semester=''@@Semester'')]/@GradeYear'
				, 'true'
			) AS sh(id int, gradeyear integer) on sh.id = sems_subj_score.ref_student_id
		WHERE 
			score <> '' and gradeyear is not null
	) as student_domain_score_table
	left outer join student on student_domain_score_table.ref_student_id = student.id
	left outer join class on student.ref_class_id = class.id
WHERE 
	score < 60
		]]>
		</Resource>
	</Resources>
</Definition>
		</Service>
		<Service Enabled="true" Name="GetUnPassStudents">
			<Definition Type="javascript">
	<Code>

var _request = getRequest();

if (_request["Request"]) _request = _request["Request"];

var result = {};

var sql = getResource('sqlstudentcount');
sql = sql.replace(/@@SchoolYear/g, _request["SchoolYear"]).replace(/@@Semester/g, _request["Semester"]);
//result.request=_request;
//result.sql=sql;
var gradeObj = {};
var rs = executeSql(sql);
while (rs.next()) {
    if (!result.GradeYear)
        result.GradeYear = [];
    var gobj = { "@Grade": rs.get("gradeyear"), "@Totle": rs.get("totle") };
    gradeObj[gobj["@Grade"]] = gobj;
    result.GradeYear.push(gobj);
}

sql = getResource('sqlunpassstudent');
sql = sql.replace(/@@SchoolYear/g, _request["SchoolYear"]).replace(/@@Semester/g, _request["Semester"]);
var gdomainObj = {};
var rs = executeSql(sql);
while (rs.next()) {
    if (!result.GradeYear)
        result.GradeYear = [];
    var gdomainKey = rs.get("gradeyear") + "^^^^" + rs.get("domain");
    if (!gdomainObj[gdomainKey]) {
        var gobj = gradeObj[rs.get("gradeyear")];
        gdobj = { "@Domain": rs.get("domain"), "@Count": 0 };
        gdomainObj[gdomainKey] = gdobj;
        if (!gobj.Domain)
            gobj.Domain = [];
        gobj.Domain.push(gdobj);
    }
    gdomainObj[gdomainKey]["@Count"]++;
    if (!gdomainObj[gdomainKey].UnPassStudent)
        gdomainObj[gdomainKey].UnPassStudent = [];
    gdomainObj[gdomainKey].UnPassStudent.push({
        "@ClassName": rs.get("class_name"),
        "@SeatNo": rs.get("seat_no"),
        "@StudentNumber": rs.get("student_number"),
        "@Name": rs.get("name"),
        "@Score": rs.get("score")
    });
}

return result;

</Code>
	<Resources>
		<Resource Name="sqlstudentcount">
			<![CDATA[
SELECT gradeyear, count(distinct ref_student_id) as totle
FROM (
	SELECT 
		sems_subj_score.ref_student_id, sh.gradeyear, sems_subj_score.school_year, sems_subj_score.semester,domain, CAST(score as decimal) as score
	FROM 
		xpath_table( 
			'id'
			, '''<root>''||score_info||''</root>'''
			, 'sems_subj_score'
			, '/root/Domains/Domain/@領域|/root/Domains/Domain/@成績'
			, 'school_year = @@SchoolYear and semester = @@Semester'
		) AS s1 (id int, domain character varying, score character varying) 
		LEFT OUTER JOIN sems_subj_score on sems_subj_score.id = s1.id
		LEFT OUTER JOIN xpath_table( 
			'id'
			, '''<root>''||sems_history||''</root>'''
			, 'student'
			, '/root/History[ ( @SchoolYear=''@@SchoolYear'' ) and (@Semester=''@@Semester'')]/@GradeYear'
			, 'true'
		) AS sh(id int, gradeyear integer) on sh.id = sems_subj_score.ref_student_id
	WHERE 
		score <> '' and gradeyear is not null
) as student_domain_score_table
GROUP BY gradeyear
		]]>
		</Resource>
		<Resource Name="sqlunpassstudent">
			<![CDATA[
SELECT gradeyear, student.id, class.class_name, student.seat_no, student.student_number, student.name, domain, score
FROM 
	(
		SELECT 
			sems_subj_score.ref_student_id, sh.gradeyear, sems_subj_score.school_year, sems_subj_score.semester,domain, CAST(score as decimal) as score
		FROM 
			xpath_table( 
				'id'
				, '''<root>''||score_info||''</root>'''
				, 'sems_subj_score'
				, '/root/Domains/Domain/@領域|/root/Domains/Domain/@成績'
				, 'school_year = @@SchoolYear and semester = @@Semester'
			) AS s1 (id int, domain character varying, score character varying) 
			LEFT OUTER JOIN sems_subj_score on sems_subj_score.id = s1.id
			LEFT OUTER JOIN xpath_table( 
				'id'
				, '''<root>''||sems_history||''</root>'''
				, 'student'
				, '/root/History[ ( @SchoolYear=''@@SchoolYear'' ) and (@Semester=''@@Semester'')]/@GradeYear'
				, 'true'
			) AS sh(id int, gradeyear integer) on sh.id = sems_subj_score.ref_student_id
		WHERE 
			score <> '' and gradeyear is not null
	) as student_domain_score_table
	left outer join student on student_domain_score_table.ref_student_id = student.id
	left outer join class on student.ref_class_id = class.id
WHERE 
	score < 60
		]]>
		</Resource>
	</Resources>
</Definition>
		</Service>
	</Package>
</Contract>
	</Property>
</Project>