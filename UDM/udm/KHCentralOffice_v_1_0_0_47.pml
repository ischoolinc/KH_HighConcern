<Project Name="KHCentralOffice">
	<Property Name="UDT">
		<Release Version="1.0.0.46" URL="udm/UDT_1_0_0_46.tcmd" />
		<Release Version="1.0.0.47" URL="udm/UDT_1_0_0_47.tcmd"></Release>
	</Property>
	<Property Name="UDS">
		<Contract Name="ischool.kh.central_office" Enabled="True">
	<Definition>
	<Authentication>
		<Passport Enabled="True">
			<IssuerList>
				<Issuer Name="ischool.kh.central_office.user">
					<CertificateProvider Type="HttpGet">
						<![CDATA[https://dsns.ischool.com.tw/j.kh.edu.tw/info/Public.GetPublicKey?parser=params&Contract=ischool.kh.central_office.user&rsptype=xmlcontent]]>
					</CertificateProvider>
				</Issuer>
			</IssuerList>
		</Passport>
	</Authentication>
</Definition>
	<Package Name="_">
		<Service Enabled="true" Name="ApproveAndLock">
			<Definition Type="JavaScript">
	<Code>
		<![CDATA[			
			var  request  =getRequest().Request || getRequest();
		   	var   applingStatus = request.ApplingStatus ;
			var   Message =request.Message ;
			 var   classID = request.ClassID;
			var   districtComment = request.DistrictComment;
			var   IsLock =request.IsLock;
			var   ClientInfo =request.ClientInfo;
			
			var sql = " \
WITH  lock_section AS( \
	UPDATE   $kh.automatic.class.lock  \
	   		SET   is_lock = "+IsLock+" \
					, last_update =now()  \
					, lock_appling_status = '"  +applingStatus +"' \
					, district_comment ='" +districtComment+"'\
   			 WHERE class_id = '"+classID+"'   \
			 RETURNING * \
),log AS ( \
	INSERT INTO log (  \
		actor \
		, action_type \
		, action  \
		, target_category   \
		, server_time    \
		, client_info   \
		, action_by    \
		, description )  \
		SELECT  \
		'高雄局端'   \
		,'Update' \
		,'局端-審核鎖班申請'  \
		,'class'   \
		,now()   \
		,'"+ClientInfo+"' \
		,'[高雄局端].審核鎖班申請'   \
		,concat ('局端審核鎖班申請：班級:「' ,lock_section.class_name  ,'」，動作:「','"+Message+"','」')   \
		FROM lock_section   \
		RETURNING *  \
		)SELECT * FROM lock_section \
		";
			var result = executeSql(sql).toArray();
			return {sql :classID};
		]]>
	</Code>
</Definition>
		</Service>
		<Service Enabled="true" Name="GetClassStudentCount">
			<Definition Type="dbhelper">           
	<Action>Select</Action>
	<SQLTemplate>
		<![CDATA[
select 
    @@FieldList 
from 
(
    select 
        class.id as g1cid
        ,class.class_name as classname1
        ,count(student.id) as studentcount 
    from 
        class 
        inner join student 
            on class.id=student.ref_class_id 
    where 
        student.status in (1,4,8)
    group by 
        g1cid,classname1
) as g1	
	inner join
	(
	    select
	        class.id as g2cid
            ,class.class_name as classname2
	        ,(
	            case when 
	                $kh.automatic.class.lock.is_lock=true 
	            then 
	                '鎖定' 
                else 
                    '' 
	            end
	        ) as lock
	        , $kh.automatic.class.lock.comment 
			, $kh.automatic.class.lock.district_comment 
			--, $kh.automatic.class.lock.district_nulock_date 
	        , $kh.automatic.class.lock.doc_no 
		     , $kh.automatic.class.lock.unauto_unlock 
	
			
			, $kh.automatic.class.lock.lock_appling_status
	    from 
	        class 
	        left join $kh.automatic.class.lock 
	            on class.id=to_number($kh.automatic.class.lock.class_id,'999999999')
	) as g2 
	    on g1.g1cid=g2.g2cid 
	inner join
	(
	    select 
	        class.id as g3cid
	        ,class.class_name as classname3
	        ,sum(number_reduce) number_reduce_sum
	        ,count($kh.automatic.placement.high.concern.ref_student_id) as number_reduce_count 
	    from 
	        $kh.automatic.placement.high.concern 
	        right join student 
	            on to_number($kh.automatic.placement.high.concern.ref_student_id,'999999999')=student.id 
	        inner join class 
	            on student.ref_class_id=class.id 
	    where student.status in(1,4,8) group by g3cid,classname3
	) as g3 
	    on g1.g1cid=g3.g3cid 
	inner join class 
	    on g1.g1cid=class.id 
	left join
	(
	    select
	        class.id as g4cid
	        ,class.ref_teacher_id as g4_ref_teacher_id
	        ,(CASE WHEN (SELECT COUNT(*) FROM tag_class left outer join tag on tag_class.ref_tag_id = tag.id WHERE tag.prefix='班級分類' AND tag.name='普通班' AND tag_class.ref_class_id = class.id ) >0 THEN 'true' else '' END ) as normal_class
	        ,(CASE WHEN (SELECT COUNT(*) FROM tag_class left outer join tag on tag_class.ref_tag_id = tag.id WHERE tag.prefix='班級分類' AND tag.name='體育班' AND tag_class.ref_class_id = class.id ) >0 THEN 'true' else '' END ) as sport_class
	        ,(CASE WHEN (SELECT COUNT(*) FROM tag_class left outer join tag on tag_class.ref_tag_id = tag.id WHERE tag.prefix='班級分類' AND tag.name='美術班' AND tag_class.ref_class_id = class.id ) >0 THEN 'true' else '' END ) as art_class
	        ,(CASE WHEN (SELECT COUNT(*) FROM tag_class left outer join tag on tag_class.ref_tag_id = tag.id WHERE tag.prefix='班級分類' AND tag.name='音樂班' AND tag_class.ref_class_id = class.id ) >0 THEN 'true' else '' END ) as music_class
	        ,(CASE WHEN (SELECT COUNT(*) FROM tag_class left outer join tag on tag_class.ref_tag_id = tag.id WHERE tag.prefix='班級分類' AND tag.name='舞蹈班' AND tag_class.ref_class_id = class.id ) >0 THEN 'true' else '' END ) as dance_class
	        ,(CASE WHEN (SELECT COUNT(*) FROM tag_class left outer join tag on tag_class.ref_tag_id = tag.id WHERE tag.prefix='班級分類' AND tag.name='資優班' AND tag_class.ref_class_id = class.id ) >0 THEN 'true' else '' END ) as gifted_class
	        ,(CASE WHEN (SELECT COUNT(*) FROM tag_class left outer join tag on tag_class.ref_tag_id = tag.id WHERE tag.prefix='班級分類' AND tag.name='資源班' AND tag_class.ref_class_id = class.id ) >0 THEN 'true' else '' END ) as resource_class
	        ,(CASE WHEN (SELECT COUNT(*) FROM tag_class left outer join tag on tag_class.ref_tag_id = tag.id WHERE tag.prefix='班級分類' AND tag.name='特教班' AND tag_class.ref_class_id = class.id ) >0 THEN 'true' else '' END ) as iep_class
	        ,(CASE WHEN (SELECT COUNT(*) FROM tag_class left outer join tag on tag_class.ref_tag_id = tag.id WHERE tag.prefix='班級分類' AND tag.name='技藝專班' AND tag_class.ref_class_id = class.id ) >0 THEN 'true' else '' END ) as skill_class
	        ,(CASE WHEN (SELECT COUNT(*) FROM tag_class left outer join tag on tag_class.ref_tag_id = tag.id WHERE tag.prefix='班級分類' AND tag.name='機構式非學校自學班' AND tag_class.ref_class_id = class.id ) >0 THEN 'true' else '' END ) as no_school_class
	    from 
	        class 
	) as g4 
	    on g1.g1cid=g4.g4cid 
	left join
	(
	    select
	        teacher.id as g5tid 
	        ,teacher.teacher_name as teacher_name	
	    from teacher 
	) as g5 
	    on g5.g5tid=g4.g4_ref_teacher_id 
	    and class.grade_year is not null
	INNER JOIN
	(
		SELECT
			class.id AS g6cid
			, class.class_name as classname6
			, SUM
			(
				CASE 
					WHEN
						student.status = 4
					THEN
						1
					ELSE
						0
				END
			) AS suspension_student_count
			, SUM
			(
				CASE
					WHEN
						student.status = 8
					THEN
						1
					ELSE
						0
				END
			) AS drop_out_student_count
		FROM
			class
			INNER JOIN student
				ON student.ref_class_id = class.id
		GROUP BY
			class.id
			, class.class_name
	) AS g6
		ON g1.g1cid = g6.g6cid
order by classname1
]]>
	</SQLTemplate>
	<ResponseRecordElement>Response/Class</ResponseRecordElement>
	<FieldList Name="FieldList" Source="Field">
		<Field Alias="ClassID" Mandatory="True" Source="ClassID" Target="g1cid" />
		<Field Alias="ClassName" Mandatory="True" Source="ClassName" Target="classname1" />
		<Field Alias="TeacherName" Mandatory="True" Source="TeacherName" Target="teacher_name" />
		<Field Alias="StudentCount" Mandatory="True" Source="StudentCount" Target="studentcount" />
		<Field Alias="Lock" Mandatory="True" Source="Lock" Target="lock" />
		<Field Alias="Comment" Mandatory="True" Source="Comment" Target="comment" />
		<Field Alias="DistrictComment" Mandatory="True" Source="DistrictComment" Target="district_comment" />
	
		<Field Alias="NumberReduceSum" Mandatory="True" Source="NumberReduceSum" Target="number_reduce_sum" />
		<Field Alias="NumberReduceCount" Mandatory="True" Source="NumberReduceCount" Target="number_reduce_count" />
		<Field Alias="ClassStudentCount" Mandatory="True" Source="ClassStudentCount" Target="(studentcount +(case when number_reduce_sum is null then 0 else number_reduce_sum end))" />
		<Field Alias="GradeYear" Mandatory="True" Source="GradeYear" Target="grade_year" />
		<Field Alias="DisplayOrder" Mandatory="True" Source="DisplayOrder" Target="display_order" />
		<Field Alias="NormalClass" Mandatory="True" Source="NormalClass" Target="normal_class" />
		<Field Alias="SportClass" Mandatory="True" Source="SportClass" Target="sport_class" />
		<Field Alias="ArtClass" Mandatory="True" Source="ArtClass" Target="art_class" />
		<Field Alias="MusicClass" Mandatory="True" Source="MusicClass" Target="music_class" />
		<Field Alias="DanceClass" Mandatory="True" Source="DanceClass" Target="dance_class" />
		<Field Alias="GiftedClass" Mandatory="True" Source="GiftedClass" Target="gifted_class" />
		<Field Alias="ResourceClass" Mandatory="True" Source="ResourceClass" Target="resource_class" />
		<Field Alias="IepClass" Mandatory="True" Source="IepClass" Target="iep_class" />
		<Field Alias="SkillClass" Mandatory="True" Source="SkillClass" Target="skill_class" />
		<Field Alias="NoSchoolClass" Mandatory="True" Source="NoSchoolClass" Target="no_school_class" />
		<Field Alias="SuspensionStudentCount" Mandatory="True" Source="SuspensionStudentCount" Target="suspension_student_count" />
		<Field Alias="DropOutStudentCount" Mandatory="True" Source="DropOutStudentCount" Target="drop_out_student_count" />
		<Field Alias="UnautoUnlock" Mandatory="True" Source="UnautoUnlock" Target="unauto_unlock" />
		<Field Alias="LockApplyStatus" Mandatory="True" Source="LockApplyStatus" Target="lock_appling_status" />
	</FieldList>
	<Conditions Name="Condition" Required="False" Source="Condition" />
	<Orders Name="Order" Source="Order" />
	<Pagination Allow="True" />
</Definition>
		</Service>
		<Service Enabled="true" Name="GetClassStudSpecial">
			<Definition Type="DBHelper">
	<Action>Select</Action>
	<SQLTemplate>
		<![CDATA[SELECT @@FieldList FROM $kh.automatic.class.special inner join student on $kh.automatic.class.special.ref_student_id = student.id WHERE @@Condition @@Order]]>
	</SQLTemplate>
	<ResponseRecordElement>Response/Student</ResponseRecordElement>
	<FieldList Name="FieldList" Source="Field">
		<Field Alias="Uid" Mandatory="True" Source="Uid" Target="$kh.automatic.class.special.uid" />
		<Field Alias="LastUpdate" Mandatory="True" Source="LastUpdate" Target="$kh.automatic.class.special.last_update" />
		<Field Alias="ClassComment" Mandatory="True" Source="ClassComment" Target="$kh.automatic.class.special.class_comment" />
		<Field Alias="ClassName" Mandatory="True" Source="ClassName" Target="$kh.automatic.class.special.class_name" />
		<Field Alias="Content" Mandatory="True" OutputType="Xml" Source="Content" Target="$kh.automatic.class.special.content" />
		<Field Alias="OldClassComment" Mandatory="True" Source="OldClassComment" Target="$kh.automatic.class.special.old_class_comment" />
		<Field Alias="OldClassId" Mandatory="True" Source="OldClassId" Target="$kh.automatic.class.special.old_class_id" />
		<Field Alias="OldClassName" Mandatory="True" Source="OldClassName" Target="$kh.automatic.class.special.old_class_name" />
		<Field Alias="RefClassId" Mandatory="True" Source="RefClassId" Target="$kh.automatic.class.special.ref_class_id" />
		<Field Alias="RefStudentId" Mandatory="True" Source="RefStudentId" Target="$kh.automatic.class.special.ref_student_id" />
		<Field Alias="StudentName" Mandatory="True" Source="StudentName" Target="student.name" />
	</FieldList>
	<Conditions Name="Condition" Required="False" Source="Condition">
		<Condition Source="Uid" Target="$kh.automatic.class.special.uid" />
		<Condition Source="LastUpdate" Target="$kh.automatic.class.special.last_update" />
		<Condition Source="ClassComment" Target="$kh.automatic.class.special.class_comment" />
		<Condition Source="ClassName" Target="$kh.automatic.class.special.class_name" />
		<Condition Source="Content" Target="$kh.automatic.class.special.content" />
		<Condition Source="OldClassComment" Target="$kh.automatic.class.special.old_class_comment" />
		<Condition Source="OldClassId" Target="$kh.automatic.class.special.old_class_id" />
		<Condition Source="OldClassName" Target="$kh.automatic.class.special.old_class_name" />
		<Condition Source="RefClassId" Target="$kh.automatic.class.special.ref_class_id" />
		<Condition Source="RefStudentId" Target="$kh.automatic.class.special.ref_student_id" />
	</Conditions>
	<Orders Name="Order" Source="Order" />
	<Pagination Allow="True" />
</Definition>
		</Service>
		<Service Enabled="true" Name="GetClassTeacherStatus">
			<Definition Type="dbhelper">
	<Action>Select</Action>
	<SQLTemplate>
		<![CDATA[SELECT @@FieldList from class 
	left join teacher on teacher.id=class.ref_teacher_id where class.grade_year in ('1','2','3','7','8','9') 
		order by class.grade_year,class.class_name]]>
	</SQLTemplate>
	<ResponseRecordElement>Response/ClassRecord</ResponseRecordElement>
	<FieldList Name="FieldList" Source="Field">
		<Field Alias="ClassID" Mandatory="True" Source="ClassID" Target="class.id" />
		<Field Alias="ClassName" Mandatory="True" Source="ClassName" Target="class.class_name" />
		<Field Alias="GradeYear" Mandatory="True" Source="GradeYear" Target="class.grade_year" />
		<Field Alias="TeacherId" Mandatory="True" Source="TeacherId" Target="class.ref_teacher_id" />
		<Field Alias="TeacherName" Mandatory="True" Source="TeacherName" Target="teacher.teacher_name" />
	</FieldList>
	<Conditions Name="Condition" Required="False" Source="Condition">
	</Conditions>
	<Orders Name="Order" Source="Order" />
	<Pagination Allow="True" />
</Definition>
		</Service>
		<Service Enabled="true" Name="GetClubCount">
			<Definition Type="dbhelper">
	<Action>Select</Action>
	<SQLTemplate>
		<![CDATA[select @@FieldList
from course 
join tag_course on course.id=tag_course.ref_course_id
join tag on tag_course.ref_tag_id=tag.id
where tag.prefix='聯課活動' and tag.name='社團' and @@Condition  @@Order]]>
	</SQLTemplate>
	<ResponseRecordElement>Response/Course</ResponseRecordElement>
	<FieldList Name="FieldList" Source="Field">
		<Field Alias="CourseID" Mandatory="True" Source="Id" Target="course.id" />
		<Field Alias="SchoolYear" Mandatory="True" Source="SchoolYear" Target="course.school_year" />
		<Field Alias="Semester" Mandatory="True" Source="Semester" Target="course.semester" />
		<Field Alias="CourseName" Mandatory="True" Source="CourseName" Target="course.course_name" />
		<Field Alias="Subject" Mandatory="True" Source="Subject" Target="course.subject" />
	</FieldList>
	<Conditions Name="Condition" Required="True" Source="Condition">
		<Condition Source="SchoolYear" Target="course.school_year" />
		<Condition Source="Semester" Target="course.semester" />
	</Conditions>
	<Orders Name="Order" Source="Order" />
	<Pagination Allow="True" />
</Definition>
		</Service>
		<Service Enabled="true" Name="GetClubStatus">
			<Definition Type="dbhelper">
	<Action>Select</Action>
	<SQLTemplate>
		<![CDATA[SELECT @@FieldList FROM $jhschool.association.udt.clubschedule clubschedule 
	join $jhschool.association.udt.clubsetting clubsetting 
		on clubschedule.grade_year=clubsetting.grade_year
			and clubschedule.school_year=clubsetting.school_year
			and clubschedule.semester=clubsetting.semester
	WHERE @@Condition @@Order]]>
	</SQLTemplate>
	<ResponseRecordElement>Response/OccurClub</ResponseRecordElement>
	<FieldList Name="FieldList" Source="Field">
		<Field Alias="SchoolYear" Mandatory="True" Source="SchoolYear" Target="clubschedule.school_year" />
		<Field Alias="Semester" Mandatory="True" Source="Semester" Target="clubschedule.semester" />
		<Field Alias="GradeYear" Mandatory="True" Source="GradeYear" Target="clubschedule.grade_year" />
		<Field Alias="OccurDate" Mandatory="True" Source="OccurDate" Target="clubschedule.occur_date" />
		<Field Alias="Period" Mandatory="True" Source="Period" Target="clubschedule.period" />
		<Field Alias="Week" Mandatory="True" Source="Week" Target="clubschedule.week" />
		<Field Alias="IsSingleDoubleWeek" Mandatory="True" Source="IsSingleDoubleWeek" Target="clubsetting.is_single_double_week" />
	</FieldList>
	<Conditions Name="Condition" Required="False" Source="Condition">
		<Condition Source="SchoolYear" Target="clubschedule.school_year" />
		<Condition Source="Semester" Target="clubschedule.semester" />
	</Conditions>
	<Orders Name="Order" Source="Order" />
	<Pagination Allow="True" />
</Definition>
		</Service>
		<Service Enabled="true" Name="GetStudentHighConcern">
			<Definition Type="dbhelper">
	<Action>Select</Action>
	<SQLTemplate>
		<![CDATA[SELECT @@FieldList FROM class inner join student on class.id=student.ref_class_id inner join $kh.automatic.placement.high.concern on student.id=to_number($kh.automatic.placement.high.concern.ref_student_id,'999999999') where student.status in(1,4,8) order by class.class_name,student.seat_no]]>
	</SQLTemplate>
	<ResponseRecordElement>Response/Student</ResponseRecordElement>
	<FieldList Name="FieldList" Source="Field">
		<Field Alias="StudentName" Mandatory="True" Source="StudentName" Target="student.name" />
		<Field Alias="ClassName" Mandatory="True" Source="ClassName" Target="class.class_name" />
		<Field Alias="SeatNo" Mandatory="True" Source="SeatNo" Target="student.seat_no" />
		<Field Alias="HighConcern" Mandatory="True" Source="HighConcern" Target="$kh.automatic.placement.high.concern.high_concern" />
		<Field Alias="NumberReduce" Mandatory="True" Source="NumberReduce" Target="$kh.automatic.placement.high.concern.number_reduce" />
		<Field Alias="DocNo" Mandatory="True" Source="DocNo" Target="$kh.automatic.placement.high.concern.doc_no" />
	</FieldList>
	<Conditions Name="Condition" Required="False" Source="Condition">
	</Conditions>
	<Orders Name="Order" Source="Order" />
	<Pagination Allow="True" />
</Definition>
		</Service>
		<Service Enabled="true" Name="ReturnApplication">
			<Definition Type="JavaScript">
	<Code>
		<![CDATA[			
			var  request  =getRequest().Request || getRequest();
		   	var   applingStatus = request.ApplingStatus;
			 var   classID = request.ClassID;
			var   districtComment = request.DistrictComment;
			var sql = " \
			UPDATE   $kh.automatic.class.lock  \
					SET last_update_by_district =true \
					, last_update =now() \
					, lock_apply_status = '"+applingStatus+"' \
					, district_comment  = '"+districtComment+"' \
   			 WHERE class_id ='" + classID+"' \
			 RETURNING * \
		";
			var result = executeSql(sql).toArray();
			return {sql :result};
		]]>
	</Code>
</Definition>
		</Service>
		<Service Enabled="true" Name="UpdateClassNulock">
			<Definition Type="JavaScript">

	<Code>
		<![CDATA[ 
		var  request  =getRequest().Request || getRequest();
 		var ClientInfo=request.ClientInfo;
		var sql = " \
		WITH  update_section  AS ( \
		UPDATE   \
		$kh.automatic.class.lock   \
		SET \
		is_lock =false  \
		, district_nulock_date = now()  \
		, unauto_unlock = false \
		, last_update =now()  \
		, district_comment ='局端解鎖'  \
		,lock_appling_status =NULL \
		WHERE \
		(unauto_unlock = false  AND is_lock = true ) \
		OR  ( lock_appling_status IS NOT NULL  AND  lock_appling_status != '' ) \
		RETURNING *  \
		),insert_log AS ( \
		INSERT INTO log ( \
		actor   \
		, action_type  \
		, action  \
		, target_category  \
		, server_time   \
		, client_info  \
		, action_by   \
		, description ) \
		SELECT \
		'高雄局端'  \
		,'Update'  \
		,'局端解鎖' \
		,'class'  \
		,now()  \
		, '"+ClientInfo+"' \
		,'[高雄局端].手動解鎖'  \
		, concat ('局端解鎖：共',(SELECT count(*) FROM update_section)||' 班。\n','\n',string_agg(update_section.class_name,'\n'))  \
		FROM update_section  \
		RETURNING * \
		) ,data AS ( \
		SELECT \
		'高雄_局端解鎖_通知設定' ::TEXT  AS name \
		, concat('<District> <LogID>', insert_log.id,'</LogID>  <IsShow>true</IsShow>  </District>')::TEXT AS content \
		FROM insert_log \
		),update_list AS \
		( \
		UPDATE list SET \
		content = data.content \
		FROM \
		data \
		WHERE \
		list.name = data.name \
		RETURNING list.* \
		) ,insert_list AS( \
		INSERT INTO list (name, content) \
		SELECT data.name, data.content \
		FROM data \
		LEFT OUTER JOIN list ON list.name = data.name \
		WHERE  \
		list.list_id is null \
		RETURNING list.*  \
		)SELECT * FROM insert_log  \
		"
		
		var result = executeSql(sql).toArray();
		return {sql :result};

	]]>
	</Code>

</Definition>
		</Service>
	</Package>
	<Package Name="data_exchange">
		<Service Enabled="true" Name="GetAllAbsenceRecords">
			<Definition Type="javascript">
	<Code>
		var _sql ;
		var _return = [];
		_sql = getResource('mainSQL');
		_return = executeSql(_sql).toArray();
		return {data: _return};
	</Code>
	<Resources>
		<Resource Name="mainSQL">
			<![CDATA[
				select ref_student_id, school_year, semester, occur_date, detail
                from attendance
                where ref_student_id in ( 
                    select id from student where status=1
                )
			]]>
		</Resource>
	</Resources>
</Definition>
		</Service>
		<Service Enabled="true" Name="GetAllAbsenceRecordsByClass">
			<Definition Type="JavaScript">
	<Code>
		<![CDATA[			
			var  request  =getRequest().Request || getRequest();
			var ClassID = request.ClassID;
			var sql = " \
			  SELECT  \
						ref_student_id \
						, school_year \
						, semester  \
						, occur_date, detail \
             FROM  \
					  	attendance \
              WHERE  \
							ref_student_id in (  SELECT  id FROM  student WHERE  status = 1 AND     ref_class_id  = "+ ClassID+" ) ";
			var result = executeSql(sql).toArray();
			return {result :result};
		]]>
	</Code>
</Definition>
		</Service>
		<Service Enabled="true" Name="GetAllDailyLife">
			<Definition Type="javascript">
	<Code>
		var _sql ;
		var _return = [];
		_sql = getResource('mainSQL');
		_return = executeSql(_sql).toArray();
		return {data: _return};
	</Code>
	<Resources>
		<Resource Name="mainSQL">
			<![CDATA[
				select ref_student_id, school_year, semester, text_score, initial_summary, summary
                from sems_moral_score
                where ref_student_id in ( 
                    select id from student where status=1
                )
			]]>
		</Resource>
	</Resources>
</Definition>
		</Service>
		<Service Enabled="true" Name="GetAllLearningService">
			<Definition Type="javascript">
	<Code>
		var _sql ;
		var _return = [];
		_sql = getResource('mainSQL');
		_return = executeSql(_sql).toArray();
		return {data: _return};
	</Code>
	<Resources>
		<Resource Name="mainSQL">
			<![CDATA[
				select ref_student_id, school_year, semester, occur_date, reason,
                            hours, organizers, internal_or_external 
                from   $k12.service.learning.record
                where ref_student_id in ( 
                    select id :: varchar from student where status=1
                )	
			]]>
		</Resource>
	</Resources>
</Definition>
		</Service>
		<Service Enabled="true" Name="GetAllMeritRecords">
			<Definition Type="javascript">
	<Code>
		var _sql ;
		var _return = [];
		_sql = getResource('mainSQL');
		_return = executeSql(_sql).toArray();
		return {data: _return};
	</Code>
	<Resources>
		<Resource Name="mainSQL">
			<![CDATA[
				select ref_student_id, school_year, semester, occur_date, merit_flag, detail, reason
                from discipline
                where ref_student_id in ( 
                    select id from student where status=1
                ) 
			]]>
		</Resource>
	</Resources>
</Definition>
		</Service>
		<Service Enabled="true" Name="GetAllSemsScore">
			<Definition Type="javascript">
	<Code>
		var _sql ;
		var _return = [];
		_sql = getResource('mainSQL');
		_return = executeSql(_sql).toArray();
		return {data: _return};
	</Code>
	<Resources>
		<Resource Name="mainSQL">
			<![CDATA[
				select ref_student_id, school_year, semester, score_info
                from   sems_subj_score
                where ref_student_id in ( 
                    select id from student where status=1
                ) 	
			]]>
		</Resource>
	</Resources>
</Definition>
		</Service>
		<Service Enabled="true" Name="GetAllStudent">
			<Definition Type="javascript">
	<Code>
		var _sql ;
		var _return = [];
		_sql = getResource('mainSQL');
		_return = executeSql(_sql).toArray();
		return {data: _return};
	</Code>
	<Resources>
		<Resource Name="mainSQL">
			<![CDATA[
					SELECT
							id
							, name
							, english_name
							, birthdate
							, id_number
							, ref_class_id
							,  mailing_address
							, permanent_address
							, contact_phone
							, gender
							, nationality
							, overseas_compatriot_from
							, seat_no
							, father_name
							, father_nationality
							, mother_name
							, mother_nationality
							, custodian_name
							, custodian_nationality
							, father_living
							, mother_living
							, sems_history 
							 , custodian_id_number
  							, father_id_number
  							, mother_id_number
         FROM student WHERE status = 1 
			]]>
		</Resource>
	</Resources>
</Definition>
		</Service>
		<Service Enabled="true" Name="GetAllStudentExtInfo">
			<Definition Type="javascript">
	<Code>
		var _sql ;
		var _return = [];
		_sql = getResource('mainSQL');
		_return = executeSql(_sql).toArray();
		return {data: _return};
	</Code>
	<Resources>
		<Resource Name="mainSQL">
			<![CDATA[
				SELECT  ref_student_id, is_handicapped, identified_date, is_gifted, is_parent_handcapped, is_dependents_from_china, 
                        is_oversea_student, is_from_hk, is_frontier, is_foreign, is_exp_staff_child,
                        is_physical_excellence, is_gov_emp_child, is_gov_emp_bereaved_official, is_gov_emp_bereaved_ill_accident, is_special_situation_family,
                        income_type, approved_date, expired_date , aboriginal_type, tribal_group, 
                        aboriginal_lang_verified, father_birth_year, father_birth_country, mother_birth_year, mother_birth_country,
                        guardian_id_number, father_id_number, mother_id_number, second_country, residence
                FROM student_info_ext 
                WHERE ref_student_id in ( SELECT id FROM student WHERE status = 1 )
			]]>
		</Resource>
	</Resources>
</Definition>
		</Service>
		<Service Enabled="true" Name="GetClassList">
			<Definition Type="javascript">
	<Code>
		var _sql ;
		var _return = [];
		_sql = getResource('mainSQL');
		_return = executeSql(_sql).toArray();
		return {data: _return};
	</Code>
	<Resources>
		<Resource Name="mainSQL">
			<![CDATA[
				SELECT  cls.id, cls.class_name, cls.display_order, cls.grade_year, tea.teacher_name
                                            FROM class cls left outer join teacher tea on cls.ref_teacher_id = tea.id
			]]>
		</Resource>
	</Resources>
</Definition>
		</Service>
		<Service Enabled="true" Name="GetSchoolData">
			<Definition Type="javascript">
	<Code>
		var _sql ;
		var _return = [];
		_sql = getResource('mainSQL');
		_return = executeSql(_sql).toArray();
		return {data: _return};
	</Code>
	<Resources>
		<Resource Name="mainSQL">
			<![CDATA[
			SELECT
				school_sems.school_year
				, school_sems.semester
				, school_info.school_name
				, school_info.school_code
            FROM
             (
				SELECT
							('0'||array_to_string(xpath('./DefaultSchoolYear/text()', xmlparse(content xmlEle)), ''))::decimal as school_year
							, ('0'||array_to_string(xpath('./DefaultSemester/text()', xmlparse(content xmlEle)), ''))::decimal as semester
					FROM (
							SELECT 
							unnest(xpath('/SystemConfig', xmlparse(content content))) as xmlEle
							FROM
							list
							WHERE 
							name = '系統設定'
								LIMIT 1
					) AS ele
             ) as school_sems
             CROSS JOIN (
					SELECT
							array_to_string(xpath('./ChineseName/text()', xmlparse(content xmlEle)), '') as school_name
							, array_to_string(xpath('./Code/text()', xmlparse(content xmlEle)), '') as school_code
					FROM (
							SELECT 
								unnest(xpath('/SchoolInformation', xmlparse(content content))) as xmlEle
							FROM
								list
							WHERE 
								name = '學校資訊'
							LIMIT 1
					) AS ele
             ) as school_info	
			]]>
		</Resource>
	</Resources>
</Definition>
		</Service>
	</Package>
	<Package Name="statistics_report">
		<Service Enabled="true" Name="GetAboriginalLangCount">
			<Definition Type="JavaScript">
	<Code>
		var _sql ;
		var _return = [];
		_sql = getResource('mainSQL');
		_return = executeSql(_sql).toArray();
		return {data: _return};
	</Code>
	<Resources>
		<Resource Name="mainSQL">
			<![CDATA[
SELECT 
   class.grade_year
  , student.gender
  , student_info_ext.tribal_group
  , aboriginal_lang_verified
   , count(*)
FROM
	( SELECT * FROM  student WHERE status =1 ) AS student
INNER JOIN
    ( SELECT *FROM 	student_info_ext  WHERE tribal_group is NOT NULL  AND (aboriginal_lang_verified IS NOT NULL AND  aboriginal_lang_verified != '無' )) AS student_info_ext
ON 	student.id  = student_info_ext.ref_student_id
 LEFT JOIN  class
 ON student.ref_class_id = class.id
  GROUP BY
  	  student.gender
	, class.grade_year
	, student_info_ext.tribal_group
	, aboriginal_lang_verified
	ORDER BY
	 	 class.grade_year  
		, student_info_ext.tribal_group
		, student.gender
			]]>
		</Resource>
	</Resources>
</Definition>
		</Service>
		<Service Enabled="true" Name="GetAboriginalTypeCount">
			<Definition Type="JavaScript">
	<Code>
		var _sql ;
		var _return = [];
		_sql = getResource('mainSQL');
		_return = executeSql(_sql).toArray();
		return {data: _return};
	</Code>
	<Resources>
		<Resource Name="mainSQL">
			<![CDATA[
SELECT 
	  student.gender
	, class.grade_year
	, student_info_ext.tribal_group
	, count(*)
FROM
	( SELECT * FROM  student WHERE status =1 ) AS student
INNER JOIN
    ( SELECT *FROM 	student_info_ext  WHERE tribal_group is NOT NULL ) AS student_info_ext
ON 	student.id  = student_info_ext.ref_student_id
 LEFT JOIN  class
 ON student.ref_class_id = class.id
  GROUP BY
  	  student.gender
	, class.grade_year
	, student_info_ext.tribal_group
	ORDER BY
	 	 class.grade_year 
		, student_info_ext.tribal_group
		, student.gender
			]]>
		</Resource>
	</Resources>
</Definition>
		</Service>
		<Service Enabled="true" Name="GetDeferStudentCount">
			<Definition Type="JavaScript">
	<Code>
		var _sql ;
		var _return = [];
		_sql = getResource('mainSQL');
		_return = executeSql(_sql).toArray();
		return {data: _return};
	</Code>
	<Resources>
		<Resource Name="mainSQL">
			<![CDATA[

WITH   row   AS(
	SELECT 
		 setting. school_year
		, setting.semester
		, student.id 
		, student.name 
		, student.gender 
		, class.grade_year 
	FROM 	
		 (	SELECT	*	FROM student WHERE 	status = 4 ) AS student 
	LEFT JOIN class 
		ON student.ref_class_id =class.id 
	CROSS JOIN
				(
				SELECT 
					('0'||array_to_string(xpath('/SystemConfig/DefaultSchoolYear/text()', xmlparse(content content)), '')::text)::decimal as school_year
					, ('0'||array_to_string(xpath('/SystemConfig/DefaultSemester/text()', xmlparse(content content)), '')::text)::decimal as semester
				FROM
					list
				WHERE 
				name = '系統設定'
		) AS setting 
)SELECT 
		school_year
		, semester
		, grade_year
		, gender
		, count(*)
FROM 
			row 
GROUP BY 
		school_year
		, semester
		, grade_year
	    , gender
			]]>
		</Resource>
	</Resources>
</Definition>
		</Service>
		<Service Enabled="true" Name="GetIncomeTypeCount">
			<Definition Type="JavaScript">
	<Code>
		var _sql ;
		var _return = [];
		_sql = getResource('mainSQL');
		_return = executeSql(_sql).toArray();
		return {data: _return};
	</Code>
	<Resources>
		<Resource Name="mainSQL">
			<![CDATA[
SELECT 
   class.grade_year
  , student.gender
  , student_info_ext.income_type
   , count(*)
FROM
	( SELECT * FROM  student WHERE status =1 ) AS student
INNER JOIN
    ( SELECT *FROM 	student_info_ext  WHERE income_type is NOT NULL  ) AS student_info_ext
	ON 	student.id  = student_info_ext.ref_student_id
 LEFT JOIN  class
 	ON student.ref_class_id = class.id
  GROUP BY
  	  student.gender
	, class.grade_year
	, student_info_ext.income_type
ORDER BY
	 	 class.grade_year 
		, student_info_ext.income_type
		, student.gender
			]]>
		</Resource>
	</Resources>
</Definition>
		</Service>
	</Package>
</Contract>
		<Contract Name="ischool.kh.CentralOffice" Enabled="True">
	<Definition>
	<Authentication>
		<Passport Enabled="true">
			<IssuerList>
				<Issuer Name="greening.shared.user">
					<CertificateProvider Type="HttpGet">https://greening.ischool.com.tw/dsa/greening/info/Public.GetPublicKey?rsptype=xmlcontent</CertificateProvider>
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
		<Service Enabled="true" Name="GetStudentReversion">
			<Definition Type="dbhelper">
	<Action>Select</Action>
	<SQLTemplate>
		<![CDATA[
SELECT 
	@@FieldList
FROM 
	(
		SELECT 
			MAX(id) AS id
			, data_id
		FROM 
			student_reversion 
		GROUP BY
			data_id
		ORDER BY 
			id
	) AS student_max_reversion
	LEFT OUTER JOIN student_reversion ON  student_reversion.id = student_max_reversion.id
	LEFT OUTER JOIN student ON student.id = student_reversion.data_id
	LEFT OUTER JOIN (
		SELECT 
			id
			, class_name
			, grade_year
			, display_order
			, CASE grade_year WHEN 1 THEN 7 WHEN 2 THEN 8 WHEN 3 THEN 9 ELSE grade_year END::text||lpad(CASE display_order is null WHEN TRUE THEN (rank() over (PARTITION BY grade_year ORDER BY class_name)) ELSE display_order END::text, 2, '0') as class_No
		FROM class
		WHERE
			class.id in (SELECT ref_class_id FROM student WHERE student.status = 1)
		ORDER BY grade_year, display_order, class_name
	) AS class ON class.id = student.ref_class_id	
WHERE @@Condition
		]]>
	</SQLTemplate>
	<ResponseRecordElement>Response/StudentReversion</ResponseRecordElement>
	<FieldList Name="FieldList" Source="Field">
		<Field Alias="StudentReversionID" Mandatory="True" Source="StudentReversionID" Target="student_reversion.id" />
		<Field Alias="DataID" Mandatory="True" Source="DataId" Target="student_reversion.data_id" />
		<Field Alias="Action" Mandatory="True" Source="Action" Target="student_reversion.action" />
		<Field Alias="ActionTime" Mandatory="True" Source="ActionTime" Target="student_reversion.timestamp" />
		<Field Alias="IDNumber" Mandatory="True" Source="IDNumber" Target="student.id_number" />
		<Field Alias="Birthday" Mandatory="True" Source="Birthday" Target="student.birthdate::date" />
		<Field Alias="Gender" Mandatory="True" Source="Gender" Target="CASE student.gender WHEN 1::bit THEN '男' ELSE '女' END" />
		<Field Alias="Name" Mandatory="True" Source="Name" Target="student.name" />
		<Field Alias="StudentNumber" Mandatory="True" Source="StudentNumber" Target="student.student_number" />
		<Field Alias="ClassNo" Mandatory="True" Source="ClassNo" Target="class.class_no" />
		<Field Alias="SeatNo" Mandatory="True" Source="SeatNo" Target="lpad(student.seat_no::text, 2, '0')" />
		<Field Alias="StatusCode" Mandatory="True" Source="StatusCode" Target="student.status" />
		<Field Alias="RefClassID" Mandatory="True" Source="RefClassID" Target="student.ref_class_id" />
		<Field Alias="GradeYear" Mandatory="True" Source="GradeYear" Target="class.grade_year" />
		<Field Alias="DisplayOrder" Mandatory="True" Source="DisplayOrder" Target="class.display_order" />
		<Field Alias="ClassName" Mandatory="True" Source="ClassName" Target="class.class_name" />
	</FieldList>
	<Conditions Name="Condition" Required="False" Source="Condition">
		<Condition Comparer="&gt;" Quote="false" Required="false" Source="StudentReversionID" Target="student_reversion.id" />
	</Conditions>
</Definition>
		</Service>
		<Service Enabled="true" Name="GetStudents">
			<Definition Type="DBHelper">
	<Action>Select</Action>
	<SQLTemplate>
		<![CDATA[
SELECT @@FieldList 
FROM student 
	LEFT OUTER JOIN tag_student on student.id=tag_student.ref_student_id 
	LEFT OUTER JOIN tag on tag.id=tag_student.ref_tag_id 
	LEFT OUTER JOIN class on class.id=student.ref_class_id				
	LEFT OUTER JOIN $kh_central.office_system.student_category_mapping on  (CASE WHEN tag.prefix='' THEN tag.name ELSE tag.prefix || ':' || tag.name  END)=$kh_central.office_system.student_category_mapping.student_category
	LEFT OUTER JOIN $kh.automatic.placement.high.concern on student.id=to_number($kh.automatic.placement.high.concern.ref_student_id,'999999999') 
	LEFT OUTER JOIN 
	(
		SELECT ct.ref_class_id, ct.name, ROW_NUMBER() OVER(PARTITION BY ct.ref_class_id ORDER BY ct.od) AS rk 
		FROM 
		(
			SELECT tag_class.ref_class_id as ref_class_id, tag.name as name, tag.id as od
			FROM tag
			LEFT OUTER JOIN tag_class on tag_class.ref_tag_id = tag.id
			WHERE prefix = '班級分類'
			UNION ALL
			SELECT class.id, '普通班', 2147483647
			FROM class
		) as ct
	) as ctl on class.id=ctl.ref_class_id
WHERE student.status=1 and ctl.rk=1
ORDER BY student.ref_class_id,student.id 
	    ]]>
	</SQLTemplate>
	<ResponseRecordElement>Students</ResponseRecordElement>
	<FieldList Name="FieldList" Source="Field">
		<Field Alias="ID" Mandatory="False" Source="ID" Target="student.id" />
		<Field Alias="Name" Mandatory="True" Source="Name" Target="student.name" />
		<Field Alias="IdNumber" Mandatory="True" Source="IdNumber" Target="student.id_number" />
		<Field Alias="birthDt" Mandatory="True" Source="birthDt" Target="student.birthdate" />
		<Field Alias="SeatNo" Mandatory="True" Source="SeatNo" Target="student.seat_no" />
		<Field Alias="StudentNumber" Mandatory="True" Source="StudentNumber" Target="student.student_number" />
		<Field Alias="Gender" Mandatory="True" OutputConverter="BitToGender" Source="Gender" Target="student.gender" />
		<Field Alias="Nationality" Mandatory="True" Source="Nationality" Target="(CASE WHEN student.nationality = '' OR student.nationality is null THEN '中華民國' ELSE student.nationality END )" />
		<Field Alias="FatherNationality" Mandatory="True" Source="FatherNationality" Target="(CASE WHEN student.father_nationality = '' OR student.father_nationality is null THEN '中華民國' ELSE student.father_nationality END )" />
		<Field Alias="MotherNationality" Mandatory="True" Source="MotherNationality" Target="(CASE WHEN student.mother_nationality = '' OR student.mother_nationality is null THEN '中華民國' ELSE student.mother_nationality END )" />
		<Field Alias="CustodianNationality" Mandatory="True" Source="CustodianNationality" Target="(CASE WHEN student.custodian_nationality = '' OR student.custodian_nationality is null THEN '中華民國' ELSE student.custodian_nationality END )" />
		<Field Alias="ClassName" Mandatory="True" Source="ClassName" Target="class.class_name" />
		<Field Alias="GradeYear" Mandatory="True" Source="GradeYear" Target="class.grade_year" />
		<Field Alias="CentralCategory" Mandatory="True" OutputType="Attribute" Source="CentralCategory" Target="central_category" />
		<Field Alias="TagID" Mandatory="False" OutputType="Attribute" Source="TagID" Target="tag.id" />
		<Field Alias="TagName" Mandatory="True" OutputType="Attribute" Source="TagName" Target="(CASE WHEN tag.prefix='' THEN tag.name ELSE tag.prefix || ':' || tag.name  END)" />
		<Field Alias="HighConcern" Mandatory="True" Source="HighConcern" Target="(case when $kh.automatic.placement.high.concern.high_concern='t' then '是' else '' end)" />
		<Field Alias="NumberReduce" Mandatory="True" Source="NumberReduce" Target="$kh.automatic.placement.high.concern.number_reduce" />
		<Field Alias="DocNo" Mandatory="True" Source="DocNo" Target="$kh.automatic.placement.high.concern.doc_no" />
		<Field Alias="class_No" Mandatory="True" Source="classNo" Target="CASE class.grade_year WHEN 1 THEN 7 WHEN 2 THEN 8 WHEN 3 THEN 9 ELSE class.grade_year END::text||lpad(CASE display_order is null WHEN TRUE THEN (dense_rank() over (PARTITION BY class.grade_year ORDER BY class.class_name)) ELSE display_order END::text, 2, '0')" />
		<Field Alias="ClassTagName" Mandatory="True" Source="ClassTagName" Target="ctl.name" />
	</FieldList>
	<ExportStyle>
		<Group Fields="ID,Name,IdNumber,birthDt,SeatNo,StudentNumber,Gender,ClassName,GradeYear,HighConcern,NumberReduce,DocNo,class_No,ClassTagName,Nationality,FatherNationality,MotherNationality,CustodianNationality" Name="Student">
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
SELECT CASE WHEN gradeyear > 6 THEN gradeyear - 6 ELSE gradeyear END as gradeyear, student.id, class.class_name, student.seat_no, student.student_number, student.name, domain, score
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