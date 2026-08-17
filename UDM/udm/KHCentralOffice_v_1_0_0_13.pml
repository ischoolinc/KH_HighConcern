<Project Name="KHCentralOffice">
	<Property Name="UDT">
		<Release Version="1.0.0.10" URL="udm/UDT_1_0_0_10.tcmd" />
		<Release Version="1.0.0.11" URL="udm/UDT_1_0_0_11.tcmd" />
		<Release Version="1.0.0.12" URL="udm/UDT_1_0_0_12.tcmd" />
		<Release Version="1.0.0.13" URL="udm/UDT_1_0_0_13.tcmd"></Release>
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
	<Code>var result = {};


var sql = getResource('sqlstudentcount');
var gradeObj = {};
var stuObj = {};
var rs = executeSql(sql);
var appendStu = {};

while (rs.next()) {
    if (!result.GradeYear)
        result.GradeYear = [];
    var gyear = rs.get("grade_year");
    if (!gradeObj[gyear]) {
        var gobj = {
            "@Grade": gyear,
            "@Totle": 0,
            "@UnPassCount": 0
        };
        gradeObj[gyear] = gobj;
        result.GradeYear.push(gobj);
    }
    gradeObj[gyear]["@Totle"]++;

    if (Number(rs.get("score")) &lt; 60) {
        if (!stuObj[rs.get("id")]) {
            var sObj = {
                "@ClassName": rs.get("class_name")
                , "@Name": rs.get("name")
                , "@SeatNo": rs.get("seat_no")
                , "@StudentNumber": rs.get("student_number")
                , "Domain": []
            };
            stuObj[rs.get("id")] = sObj;
        }
        var sObj = stuObj[rs.get("id")];
        sObj.Domain.push({
            "@Domain": rs.get("domain")
            , "@Score": rs.get("score")
        });
        if (!appendStu[rs.get("id")] &amp;&amp; sObj.Domain.length &gt;= 4) {
            appendStu[rs.get("id")] = true;
            gradeObj[gyear]["@UnPassCount"]++;
            if (!gradeObj[gyear].UnPassStudent)
                gradeObj[gyear].UnPassStudent = [];
            gradeObj[gyear].UnPassStudent.push(sObj);
        }
    }
}


return result;

	</Code>
	<Resources>
		<Resource Name="sqlstudentcount">
			<![CDATA[
SELECT 
	CASE WHEN class.grade_year > 6 THEN class.grade_year - 6 ELSE class.grade_year END as grade_year
	, class.class_name
	, student.seat_no
	, student.id
	, student.name
	, student.student_number
	, domain
	, score
FROM 
	student
	LEFT OUTER JOIN class on class.id = student.ref_class_id
	LEFT OUTER JOIN (
		SELECT grade_year
			, domain
			, id
			, avg(score) as score
			, CASE domain 
				WHEN '語文' THEN 0 
				WHEN '數學' THEN 1 
				WHEN '社會' THEN 2 
				WHEN '自然與生活科技' THEN 3 
				WHEN '藝術與人文' THEN 4 
				WHEN '健康與體育' THEN 5 
				WHEN '綜合活動' THEN 6
				ELSE 10
			END AS drank
		FROM (
			SELECT 	
				grade_year,
				id,
				school_year,
				semester,
				'語文' as domain,
				sum(powscore)/sum(credit) as score
			FROM (
				SELECT 
					class.grade_year,
					student.id,
					sems_subj_score.school_year,
					sems_subj_score.semester,
					x.domain,
					x.credit,
					CAST( regexp_replace( x.score, '^$', '0') as decimal) * x.credit as powscore
				FROM 
					student
					LEFT OUTER JOIN class on class.id = student.ref_class_id and student.status = 1 and class.grade_year is not null
					LEFT OUTER JOIN (
						SELECT student.id
							, ''||g1.SchoolYear as schoolyear1
							, ''||g2.SchoolYear as schoolyear2
							, ''||g3.SchoolYear as schoolyear3
							, ''||g4.SchoolYear as schoolyear4
							, ''||g5.SchoolYear as schoolyear5
							, ''||g6.SchoolYear as schoolyear6
						FROM 
						    student 
						    left outer join (SELECT id, max(SchoolYear) as SchoolYear FROM xpath_table( 'id', '''<root>''||sems_history||''</root>''', 'student', '/root/History[ ( @GradeYear=''7'' or @GradeYear=''1'' ) and (@Semester=''1'')]/@SchoolYear', 'id IN ( select student.id from student LEFT OUTER JOIN class ON student.ref_class_id = class.id WHERE student.status=1 AND class.grade_year is not null )') AS tmp(id int, SchoolYear integer) group by id )as g1 on g1.id=student.id left outer join (SELECT id, max(SchoolYear) as SchoolYear FROM xpath_table( 'id', '''<root>''||sems_history||''</root>''', 'student', '/root/History[ ( @GradeYear=''7'' or @GradeYear=''1'' ) and (@Semester=''2'')]/@SchoolYear', 'id IN ( select student.id from student LEFT OUTER JOIN class ON student.ref_class_id = class.id WHERE student.status=1 AND class.grade_year is not null )') AS tmp(id int, SchoolYear integer) group by id )as g2 on g2.id=student.id left outer join (SELECT id, max(SchoolYear) as SchoolYear FROM xpath_table( 'id', '''<root>''||sems_history||''</root>''', 'student', '/root/History[ ( @GradeYear=''8'' or @GradeYear=''2'' ) and (@Semester=''1'')]/@SchoolYear', 'id IN ( select student.id from student LEFT OUTER JOIN class ON student.ref_class_id = class.id WHERE student.status=1 AND class.grade_year is not null )') AS tmp(id int, SchoolYear integer) group by id )as g3 on g3.id=student.id left outer join (SELECT id, max(SchoolYear) as SchoolYear FROM xpath_table( 'id', '''<root>''||sems_history||''</root>''', 'student', '/root/History[ ( @GradeYear=''8'' or @GradeYear=''2'' ) and (@Semester=''2'')]/@SchoolYear', 'id IN ( select student.id from student LEFT OUTER JOIN class ON student.ref_class_id = class.id WHERE student.status=1 AND class.grade_year is not null )') AS tmp(id int, SchoolYear integer) group by id )as g4 on g4.id=student.id left outer join (SELECT id, max(SchoolYear) as SchoolYear FROM xpath_table( 'id', '''<root>''||sems_history||''</root>''', 'student', '/root/History[ ( @GradeYear=''9'' or @GradeYear=''3'' ) and (@Semester=''1'')]/@SchoolYear', 'id IN ( select student.id from student LEFT OUTER JOIN class ON student.ref_class_id = class.id WHERE student.status=1 AND class.grade_year is not null )') AS tmp(id int, SchoolYear integer) group by id )as g5 on g5.id=student.id left outer join (SELECT id, max(SchoolYear) as SchoolYear FROM xpath_table( 'id', '''<root>''||sems_history||''</root>''', 'student', '/root/History[ ( @GradeYear=''9'' or @GradeYear=''3'' ) and (@Semester=''2'')]/@SchoolYear', 'id IN ( select student.id from student LEFT OUTER JOIN class ON student.ref_class_id = class.id WHERE student.status=1 AND class.grade_year is not null )') AS tmp(id int, SchoolYear integer) group by id )as g6 on g6.id=student.id LEFT OUTER JOIN class ON student.ref_class_id = class.id WHERE student.status=1 AND class.grade_year is not null
					)shistory on student.id=shistory.id
					LEFT OUTER JOIN sems_subj_score on sems_subj_score.ref_student_id = student.id 
						and (
						    (''||sems_subj_score.school_year=shistory.schoolyear1 and sems_subj_score.semester= 1)
						    or (''||sems_subj_score.school_year=shistory.schoolyear2 and sems_subj_score.semester= 2)
						    or (''||sems_subj_score.school_year=shistory.schoolyear3 and sems_subj_score.semester= 1)
						    or (''||sems_subj_score.school_year=shistory.schoolyear4 and sems_subj_score.semester= 2)
						    or (''||sems_subj_score.school_year=shistory.schoolyear5 and sems_subj_score.semester= 1)
						    or (''||sems_subj_score.school_year=shistory.schoolyear6 and sems_subj_score.semester= 2)
						)
					LEFT OUTER JOIN xpath_table( 
						'id'
						, '''<root>''||score_info||''</root>'''
						, 'sems_subj_score'
						, '/root/SemesterSubjectScoreInfo/Subject[@領域=''國語文'' or @領域=''英語'']/@領域|/root/SemesterSubjectScoreInfo/Subject[@領域=''國語文'' or @領域=''英語'']/@成績|/root/SemesterSubjectScoreInfo/Subject[@領域=''國語文'' or @領域=''英語'']/@權數'
						, 'ref_student_id in (
							SELECT student.id 
							FROM
								student
								LEFT OUTER JOIN class on class.id = student.ref_class_id 
							WHERE
								student.status = 1 and class.grade_year is not null
						)'
					) AS x (id int, domain character varying, score character varying, credit decimal) on sems_subj_score.id = x.id
				WHERE 
					x.score is not null and x.score <> '' and x.credit is not null
			)as v
			GROUP BY
				grade_year,
				id,
				school_year,
				semester

			UNION ALL

			SELECT 
				class.grade_year,
				student.id,
				sems_subj_score.school_year,
				sems_subj_score.semester,
				x.domain,
				CAST( regexp_replace( x.score, '^$', '0') as decimal) as score
			FROM 
				student
				LEFT OUTER JOIN class on class.id = student.ref_class_id and student.status = 1 and class.grade_year is not null
				LEFT OUTER JOIN (
					SELECT student.id
						, ''||g1.SchoolYear as schoolyear1
						, ''||g2.SchoolYear as schoolyear2
						, ''||g3.SchoolYear as schoolyear3
						, ''||g4.SchoolYear as schoolyear4
						, ''||g5.SchoolYear as schoolyear5
						, ''||g6.SchoolYear as schoolyear6
					FROM 
					    student 
					    left outer join (SELECT id, max(SchoolYear) as SchoolYear FROM xpath_table( 'id', '''<root>''||sems_history||''</root>''', 'student', '/root/History[ ( @GradeYear=''7'' or @GradeYear=''1'' ) and (@Semester=''1'')]/@SchoolYear', 'id IN ( select student.id from student LEFT OUTER JOIN class ON student.ref_class_id = class.id WHERE student.status=1 AND class.grade_year is not null )') AS tmp(id int, SchoolYear integer) group by id )as g1 on g1.id=student.id left outer join (SELECT id, max(SchoolYear) as SchoolYear FROM xpath_table( 'id', '''<root>''||sems_history||''</root>''', 'student', '/root/History[ ( @GradeYear=''7'' or @GradeYear=''1'' ) and (@Semester=''2'')]/@SchoolYear', 'id IN ( select student.id from student LEFT OUTER JOIN class ON student.ref_class_id = class.id WHERE student.status=1 AND class.grade_year is not null )') AS tmp(id int, SchoolYear integer) group by id )as g2 on g2.id=student.id left outer join (SELECT id, max(SchoolYear) as SchoolYear FROM xpath_table( 'id', '''<root>''||sems_history||''</root>''', 'student', '/root/History[ ( @GradeYear=''8'' or @GradeYear=''2'' ) and (@Semester=''1'')]/@SchoolYear', 'id IN ( select student.id from student LEFT OUTER JOIN class ON student.ref_class_id = class.id WHERE student.status=1 AND class.grade_year is not null )') AS tmp(id int, SchoolYear integer) group by id )as g3 on g3.id=student.id left outer join (SELECT id, max(SchoolYear) as SchoolYear FROM xpath_table( 'id', '''<root>''||sems_history||''</root>''', 'student', '/root/History[ ( @GradeYear=''8'' or @GradeYear=''2'' ) and (@Semester=''2'')]/@SchoolYear', 'id IN ( select student.id from student LEFT OUTER JOIN class ON student.ref_class_id = class.id WHERE student.status=1 AND class.grade_year is not null )') AS tmp(id int, SchoolYear integer) group by id )as g4 on g4.id=student.id left outer join (SELECT id, max(SchoolYear) as SchoolYear FROM xpath_table( 'id', '''<root>''||sems_history||''</root>''', 'student', '/root/History[ ( @GradeYear=''9'' or @GradeYear=''3'' ) and (@Semester=''1'')]/@SchoolYear', 'id IN ( select student.id from student LEFT OUTER JOIN class ON student.ref_class_id = class.id WHERE student.status=1 AND class.grade_year is not null )') AS tmp(id int, SchoolYear integer) group by id )as g5 on g5.id=student.id left outer join (SELECT id, max(SchoolYear) as SchoolYear FROM xpath_table( 'id', '''<root>''||sems_history||''</root>''', 'student', '/root/History[ ( @GradeYear=''9'' or @GradeYear=''3'' ) and (@Semester=''2'')]/@SchoolYear', 'id IN ( select student.id from student LEFT OUTER JOIN class ON student.ref_class_id = class.id WHERE student.status=1 AND class.grade_year is not null )') AS tmp(id int, SchoolYear integer) group by id )as g6 on g6.id=student.id LEFT OUTER JOIN class ON student.ref_class_id = class.id WHERE student.status=1 AND class.grade_year  is not null
				)shistory on student.id=shistory.id
				LEFT OUTER JOIN sems_subj_score on sems_subj_score.ref_student_id = student.id 
					and (
					    (''||sems_subj_score.school_year=shistory.schoolyear1 and sems_subj_score.semester= 1)
					    or (''||sems_subj_score.school_year=shistory.schoolyear2 and sems_subj_score.semester= 2)
					    or (''||sems_subj_score.school_year=shistory.schoolyear3 and sems_subj_score.semester= 1)
					    or (''||sems_subj_score.school_year=shistory.schoolyear4 and sems_subj_score.semester= 2)
					    or (''||sems_subj_score.school_year=shistory.schoolyear5 and sems_subj_score.semester= 1)
					    or (''||sems_subj_score.school_year=shistory.schoolyear6 and sems_subj_score.semester= 2)
					)
				LEFT OUTER JOIN xpath_table( 
					'id'
					, '''<root>''||score_info||''</root>'''
					, 'sems_subj_score'
					, '/root/Domains/Domain/@領域|/root/Domains/Domain/@成績'
					, 'ref_student_id in (
						SELECT student.id 
						FROM
							student
							LEFT OUTER JOIN class on class.id = student.ref_class_id 
						WHERE
							student.status = 1 and class.grade_year is not null
					)'
				) AS x (id int, domain character varying, score character varying) on sems_subj_score.id = x.id
			WHERE 
				x.score is not null and x.score <> '' and domain <> '國語文' and domain <> '英語'
		)as val
		GROUP BY grade_year, domain, id
		ORDER BY id, domain
	) as gradeScore on gradeScore.id = student.id
WHERE student.status = 1 and class.grade_year is not null
ORDER BY class.grade_year, class.display_order, class.class_name, student.seat_no, student.id, drank
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
			id as ref_student_id,
			gradeyear,
			school_year,
			semester,
			'語文' as domain,
			sum(powscore)/sum(credit) as score
		FROM (
			SELECT 
				sems_subj_score.ref_student_id as id,
				sh.gradeyear, 
				sems_subj_score.school_year,
				sems_subj_score.semester,
				x.domain,
				x.credit,
				CAST( regexp_replace( x.score, '^$', '0') as decimal) * x.credit as powscore
			FROM 
				xpath_table( 
					'id'
					, '''<root>''||score_info||''</root>'''
					, 'sems_subj_score'
					, '/root/SemesterSubjectScoreInfo/Subject[@領域=''國語文'' or @領域=''英語'']/@領域|/root/SemesterSubjectScoreInfo/Subject[@領域=''國語文'' or @領域=''英語'']/@成績|/root/SemesterSubjectScoreInfo/Subject[@領域=''國語文'' or @領域=''英語'']/@權數'
					, 'school_year = @@SchoolYear and semester = @@Semester'
				) AS x (id int, domain character varying, score character varying, credit decimal) 
				LEFT OUTER JOIN	sems_subj_score on sems_subj_score.id = x.id
				LEFT OUTER JOIN xpath_table( 
					'id'
					, '''<root>''||sems_history||''</root>'''
					, 'student'
					, '/root/History[ ( @SchoolYear=''@@SchoolYear'' ) and (@Semester=''@@Semester'')]/@GradeYear'
					, 'true'
				) AS sh(id int, gradeyear integer) on sh.id = sems_subj_score.ref_student_id
			WHERE 
				x.score is not null and x.score <> '' and x.credit is not null
				AND gradeyear is not null
		) as lengdomain
		GROUP BY
			id,
			gradeyear,
			school_year,
			semester
		HAVING sum(powscore)/sum(credit) < 60
		
		UNION ALL	
		
	
		SELECT 
			sems_subj_score.ref_student_id, 
			sh.gradeyear, 
			sems_subj_score.school_year, 
			sems_subj_score.semester,
			domain, 
			CAST(score as decimal) as score
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
			score <> '' 
			AND gradeyear is not null
			AND CAST(score as decimal) < 60
	) as student_domain_score_table
	left outer join student on student_domain_score_table.ref_student_id = student.id
	left outer join class on student.ref_class_id = class.id
		]]>
		</Resource>
	</Resources>
</Definition>
		</Service>
	</Package>
</Contract>
	</Property>
</Project>