use passport;

SELECT * FROM user_data;

-- CREATE TABLE FOR STORED USER --

CREATE TABLE user_data(
	id INT PRIMARY KEY IDENTITY,
	given_name VARCHAR(30) NOT NULL,
	sure_name VARCHAR(30),
	email_id VARCHAR(30) UNIQUE NOT NULL,
	login_id VARCHAR(20) UNIQUE NOT NULL,
	login_password VARCHAR(30) NOT NULL
)

----------CREATE PROCEDURE FOR NEW USER------
ALTER PROCEDURE SPC_User(
	@GivenName VARCHAR(30),
	@SureName VARCHAR(30) = NULL,
	@EmailID VARCHAR(30),
	@LoginID VARCHAR(20),
	@LoginPassword VARCHAR(30)
)
AS
BEGIN
	INSERT INTO user_data(given_name,sure_name,email_id,login_id,login_password) 
			VALUES(@GivenName,@SureName,@EmailID,@LoginID,@LoginPassword)
END;

----------CREATE PROCEDURE FOR USER LOGIN------

ALTER PROCEDURE SPL_User(
	@LoginID VARCHAR(20),
	@LoginPassword VARCHAR(30)
)
AS
BEGIN
	SELECT * FROM user_data WHERE login_id = @LoginID AND login_password = @LoginPassword
END;

-- CREATE TABLE FOR STORED ADMIN --

CREATE TABLE admin_data(
	id INT PRIMARY KEY IDENTITY,
	admin_name VARCHAR(30) NOT NULL,
	email_id VARCHAR(30) UNIQUE NOT NULL,
	admin_id VARCHAR(20) UNIQUE NOT NULL,
	admin_password VARCHAR(30) NOT NULL
)

SELECT * FROM admin_data

----------CREATE PROCEDURE FOR GET ALL ADMIN------

CREATE PROCEDURE SPA_Admin
AS
BEGIN
	SELECT * FROM admin_data
END

----------CREATE PROCEDURE FOR NEW ADMIN------

CREATE PROCEDURE SPC_Admin
	@AdminName VARCHAR(30),
	@EmailID VARCHAR(30),
	@AdminID VARCHAR(20),
	@AdminPassword VARCHAR(20)
AS
BEGIN
	INSERT INTO admin_data(admin_name,email_id,admin_id,admin_password)
			VALUES(@AdminName,@EmailID,@AdminID,@AdminPassword)
END

----------CREATE PROCEDURE FOR ADMIN LOGIN------

CREATE PROCEDURE SPL_Admin(
	@AdminID VARCHAR(20),
	@Password VARCHAR(20)
)
AS
BEGIN
	SELECT * FROM admin_data WHERE admin_id = @AdminID AND admin_password = @Password
END;

----------CREATE TABLE FOR OFFICER------

CREATE TABLE police_data(
	id INT PRIMARY KEY IDENTITY,
	officer_name VARCHAR(30) NOT NULL,
	email_id VARCHAR(30) UNIQUE NOT NULL,
	officer_id VARCHAR(20) UNIQUE NOT NULL,
	officer_password VARCHAR(30) NOT NULL
)

----------CREATE PROCEDURE FOR OFFICER LOGIN------

CREATE PROCEDURE SPL_Officer(
	@OfficerID VARCHAR(20),
	@Password VARCHAR(20)
)
AS
BEGIN
	SELECT * FROM police_data WHERE officer_id = @OfficerID AND officer_password = @Password
END;
