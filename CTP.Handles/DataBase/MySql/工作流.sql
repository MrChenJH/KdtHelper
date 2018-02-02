/*实例流程表*/
/*==============================================================*/
/* Table: kdt_wf_instance
/*==============================================================*/

drop table if exists kdt_wf_instance

create table kdt_wf_instance(
   	auto_no INT NOT NULL AUTO_INCREMENT,
   	instance_id VARCHAR(16) NOT NULL,
   	instance_name VARCHAR(50) NOT NULL,
	instance_source VARCHAR(30) ,
	map_id VARCHAR(30) ,
	flow_id VARCHAR(16) NOT NULL,
	step_id TINYINT NOT NULL,
	instance_status TINYINT NOT NULL,
	creator VARCHAR(30) NOT NULL,
	create_time VARCHAR(20) NOT NULL,
   	PRIMARY KEY ( auto_no )
);

/*流程信息表*/
/*==============================================================*/
/* Table: kdt_wf_info*/
/*==============================================================*/

drop table if exists kdt_wf_info

create table kdt_wf_info(
   	auto_no INT NOT NULL AUTO_INCREMENT,
	flow_id VARCHAR(16) NOT NULL,
	flow_name VARCHAR(50) NOT NULL,
	flow_category VARCHAR(200) ,
	flow_note VARCHAR(200) NOT NULL,
	is_sys TINYINT not NULL,
	creator VARCHAR(30) NOT NULL,
	create_time VARCHAR(20) NOT NULL,
   	PRIMARY KEY ( auto_no )
);

/*流程步骤表*/
/*==============================================================*/
/* Table: kdt_wf_step
/*==============================================================*/

drop table if exists kdt_wf_step

create table kdt_wf_step(
   	auto_no INT NOT NULL AUTO_INCREMENT,
   	step_id TINYINT NOT NULL,
   	flow_id VARCHAR(16) NOT NULL,
		step_name VARCHAR(50) not null,
		step_back TINYINT not null ,
		step_pre TINYINT NOT NULL,
		has_next TINYINT NOT NULL,
		is_multi TINYINT NOT NULL,
		step_type TINYINT NOT NULL,
		action_id VARCHAR(16) NOT NULL,
		data_temp text,
		creator VARCHAR(30) NOT NULL,
		create_time VARCHAR(20) NOT NULL,
			PRIMARY KEY ( auto_no )
);

/*流程执行*/
/*==============================================================*/
/* Table: kdt_wf_action
/*==============================================================*/

drop table if exists kdt_wf_action

create table kdt_wf_action(
   	auto_no INT NOT NULL AUTO_INCREMENT,
   	action_id VARCHAR(16) NOT NULL,
   	action_type TINYINT NOT NULL,
		action_name VARCHAR(50) not null,
		action_category VARCHAR(200) ,
		audit_type TINYINT not null ,
		audit_mapid VARCHAR(50) NOT NULL,
		audit_position_id VARCHAR(16) NOT NULL,
		hasproxy TINYINT NOT NULL,
		proxy_type TINYINT ,
		proxy_mapid VARCHAR(50) ,
		proxy_position_id VARCHAR(16) ,
		hascopy TINYINT NOT NULL,
		copy_type TINYINT ,
		copy_mapid VARCHAR(50) ,
		copy_position_id VARCHAR(16) ,
		creator VARCHAR(30) NOT NULL,
		create_time VARCHAR(20) NOT NULL,
			PRIMARY KEY ( auto_no )
);

/*流程处理*/
/*==============================================================*/
/* Table: kdt_wf_proc
/*==============================================================*/

drop table if exists kdt_wf_proc

create table kdt_wf_proc(
   	auto_no INT NOT NULL AUTO_INCREMENT,
   	instance_id VARCHAR(16) NOT NULL,
   	step_id TINYINT NOT NULL,
		action_id VARCHAR(16) not null,
		step_emp_id VARCHAR(50)  ,
		action_time VARCHAR(20) ,
		step_note VARCHAR(300) ,
		action_status TINYINT NOT NULL,
		creator VARCHAR(30) NOT NULL,
		create_time VARCHAR(20) NOT NULL,
			PRIMARY KEY ( auto_no )
);

/*流程历史记录*/
/*==============================================================*/
/* Table: kdt_wf_history
/*==============================================================*/

drop table if exists kdt_wf_history

create table kdt_wf_history(
   	auto_no INT NOT NULL AUTO_INCREMENT,
   	instance_id VARCHAR(16) NOT NULL,
   	step_id TINYINT NOT NULL,
		action_id VARCHAR(16) not null,
		step_emp_id VARCHAR(50)  ,
		action_time VARCHAR(20) ,
		step_note VARCHAR(300) ,
		action_status TINYINT NOT NULL,
		creator VARCHAR(30) NOT NULL,
		create_time VARCHAR(20) NOT NULL,
			PRIMARY KEY ( auto_no )
);

/*流程消息*/
/*==============================================================*/
/* Table: kdt_wf_msg
/*==============================================================*/

drop table if exists kdt_wf_msg

create table kdt_wf_msg(
   	auto_no INT NOT NULL AUTO_INCREMENT,
   	instance_id VARCHAR(16) NOT NULL,
   	step_id TINYINT NOT NULL,
		action_id VARCHAR(16) not null,
		step_rec_id VARCHAR(50) not null ,
		send_on TINYINT not null,
		send_time VARCHAR(20) not null,
		send_status TINYINT NOT NULL,
		read_status TINYINT NOT NULL,
		creator VARCHAR(30) NOT NULL,
		create_time VARCHAR(20) NOT NULL,
			PRIMARY KEY ( auto_no )
);

/*流程职位*/
/*==============================================================*/
/* Table: kdt_wf_position
/*==============================================================*/

drop table if exists kdt_wf_position

create table kdt_wf_position(
   	auto_no INT NOT NULL AUTO_INCREMENT,
   	position_id VARCHAR(16) NOT NULL,
   	position_name VARCHAR(50) NOT NULL,
		is_sys TINYINT not null,
		creator VARCHAR(30) NOT NULL,
		create_time VARCHAR(20) NOT NULL,
			PRIMARY KEY ( auto_no )
);