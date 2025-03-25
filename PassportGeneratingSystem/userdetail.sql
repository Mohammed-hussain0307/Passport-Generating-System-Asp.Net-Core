use passport;

ALTER TABLE user_detail ADD message_info VARCHAR(50)
ALTER TABLE user_detail ADD passport_number VARCHAR(10)

CREATE TABLE user_detail(
	id INT PRIMARY KEY IDENTITY,
    givenName VARCHAR(30) NOT NULL,
    sureName VARCHAR(30) NOT NULL,
    gender VARCHAR(10) NOT NULL,
    dateOfBirth DATE NOT NULL,
    placeOfBirth VARCHAR(30),
    maritalStatus VARCHAR(20),
    employmentType VARCHAR(30),
    educationQualification VARCHAR(30),
    aadhaarNumber BIGINT UNIQUE,
    
    fatherGivenName VARCHAR(30),
    fatherSureName VARCHAR(30),
    motherGivenName VARCHAR(30),
    motherSureName VARCHAR(30),
    spousesGivenName VARCHAR(30),
    spousesSureName VARCHAR(30),
    
    houseStreet VARCHAR(255) NOT NULL,
    villageTownCity VARCHAR(30) NOT NULL,
    addressState VARCHAR(30) NOT NULL,
    addressDistrict VARCHAR(30) NOT NULL,
    policeStation VARCHAR(30) NOT NULL,
    pincode VARCHAR(10) NOT NULL,
    mobileNumber BIGINT NOT NULL,
    emailID VARCHAR(50),
    
    contactName VARCHAR(30) NOT NULL,
    contactMobileNumber BIGINT NOT NULL,

	status VARCHAR(20),
	admin_status VARCHAR(20),
	officer_status VARCHAR(20),

	user_id INT,
	FOREIGN KEY(user_id) REFERENCES user_data(id)
);

-- ADD USER IN THE TABLE ---

ALTER PROCEDURE SPC_UserDetail(
    @GivenName VARCHAR(30),
    @SureName VARCHAR(30),
    @Gender VARCHAR(10),
    @DateOfBirth DATE,
    @PlaceOfBirth VARCHAR(30) = NULL,
    @MaritalStatus VARCHAR(20),
    @EmploymentType VARCHAR(30) = NULL,
    @EducationQualification VARCHAR(30) = NULL,
    @AadhaarNumber BIGINT = NULL,
    
    @FatherGivenName VARCHAR(30),
    @FatherSureName VARCHAR(30) = NULL,
    @MotherGivenName VARCHAR(30),
    @MotherSureName VARCHAR(30) = NULL,
    @SpousesGivenName VARCHAR(30) = NULL,
    @SpousesSureName VARCHAR(30) = NULL,
    
    @HouseStreet VARCHAR(255),
    @VillageTownCity VARCHAR(30),
    @AddressState VARCHAR(30),
    @AddressDistrict VARCHAR(30),
    @PoliceStation VARCHAR(30),
    @Pincode VARCHAR(10),
    @MobileNumber BIGINT,
    @EmailID VARCHAR(50) = NULL,
    
    @ContactName VARCHAR(30),
    @ContactMobileNumber BIGINT,

	@UserID INT
)
AS
BEGIN
	INSERT INTO user_detail(givenName,sureName,gender,dateOfBirth,placeOfBirth,maritalStatus,employmentType,educationQualification,aadhaarNumber,
							fatherGivenName,fatherSureName,motherGivenName,motherSureName,spousesGivenName,spousesSureName,
							houseStreet,villageTownCity,addressState,addressDistrict,policeStation,pincode,mobileNumber,emailID,
							contactName,contactMobileNumber,user_id)
						VALUES(@GivenName,@SureName,@Gender,@DateOfBirth,@PlaceOfBirth,@MaritalStatus,@EmploymentType,@EducationQualification,@AadhaarNumber,
							@FatherGivenName,@FatherSureName,@MotherGivenName,@MotherSureName,@SpousesGivenName,@SpousesSureName,
							@HouseStreet,@VillageTownCity,@AddressState,@AddressDistrict,@PoliceStation,@Pincode,@MobileNumber,@EmailID,
							@ContactName,@ContactMobileNumber,@UserID)
END;

-- READ APPLICATION BASED ON LOGIN USER FROM THE TABLE --

ALTER PROCEDURE SPR_AllUser @UserID INT
AS
BEGIN
	SELECT * FROM user_detail
	WHERE user_id = @UserID AND status IS NULL
END;

EXEC SPR_AllUser @UserID = 2

-- GET THE SINGLE USER BY ID -- 

CREATE PROCEDURE SPG_User 
	@ID INT
AS
BEGIN
	SELECT * FROM user_detail WHERE id = @ID
END;

EXEC SPG_User @ID = 6

-- UPDATE USER DETAILS --

ALTER PROCEDURE SPU_User(
	@ID INT,
	@GivenName VARCHAR(30),
    @SureName VARCHAR(30),
    @Gender VARCHAR(10),
    @DateOfBirth DATE,
    @PlaceOfBirth VARCHAR(30) = NULL,
    @MaritalStatus VARCHAR(20),
    @EmploymentType VARCHAR(30) = NULL,
    @EducationQualification VARCHAR(30) = NULL,
    @AadhaarNumber BIGINT = NULL,
    
    @FatherGivenName VARCHAR(30),
    @FatherSureName VARCHAR(30) = NULL,
    @MotherGivenName VARCHAR(30),
    @MotherSureName VARCHAR(30) = NULL,
    @SpousesGivenName VARCHAR(30) = NULL,
    @SpousesSureName VARCHAR(30) = NULL,
    
    @HouseStreet VARCHAR(255),
    @VillageTownCity VARCHAR(30),
    @AddressState VARCHAR(30),
    @AddressDistrict VARCHAR(30),
    @PoliceStation VARCHAR(30),
    @Pincode VARCHAR(10),
    @MobileNumber BIGINT,
    @EmailID VARCHAR(50) = NULL,
    
    @ContactName VARCHAR(30),
    @ContactMobileNumber BIGINT
)
AS
BEGIN
	UPDATE user_detail 
	SET givenName = @GivenName,
		sureName = @SureName,
		gender = @Gender,
		dateOfBirth = @DateOfBirth,
		placeOfBirth = @PlaceOfBirth,
		maritalStatus = @MaritalStatus,
		employmentType = @EmploymentType,
		educationQualification = @EducationQualification,
		aadhaarNumber = @AadhaarNumber,    
		fatherGivenName = @FatherGivenName,
		fatherSureName = @FatherSureName,
		motherGivenName = @MotherGivenName,
		motherSureName = @MotherSureName,
		spousesGivenName = @SpousesGivenName,
		spousesSureName = @SpousesSureName,
		houseStreet = @HouseStreet,
		villageTownCity = @VillageTownCity,
		addressState = @AddressState,
		addressDistrict = @AddressDistrict,
		policeStation = @PoliceStation,
		pincode = @Pincode,
		mobileNumber = @MobileNumber,
		emailID = @EmailID,    
		contactName = @ContactName,
		contactMobileNumber = @ContactMobileNumber
		WHERE id = @ID
END;

-- DELETE USER DETAIL FROM THE TABLE --

CREATE PROCEDURE SPD_User @ID INT
AS
BEGIN
	DELETE FROM user_detail WHERE id = @ID
END;

ALTER PROCEDURE SPB_User(
	@ID INT,
	@Status VARCHAR(20)
)
AS
BEGIN
	UPDATE user_detail 
	SET status = @Status
	WHERE id = @ID
END;

-- READ ALL USER SHOW IN ADMIN PAGE BASED ON USER STATUS -- 

ALTER PROCEDURE SPA_User
AS
BEGIN
	SELECT * FROM user_detail 
	WHERE status = 'submit' AND (admin_status = 'NULL' OR admin_status IS NULL)
END;

EXEC SPA_User

-- UPDATE ADMIN STATUS AFTER APPROVED THE USER FORM --

ALTER PROCEDURE SPAP_admin 
	@ID INT,
	@MessageInfo VARCHAR(50)
AS
BEGIN
	UPDATE user_detail 
	SET admin_status = 'approve', message_info = @MessageInfo 
	WHERE id = @ID
END;

-- UPDATE ADMIN STATUS AFTER REJECTED THE USER FORM --

ALTER PROCEDURE SPRE_Admin @ID INT
AS
BEGIN
	UPDATE user_detail 
	SET admin_status = 'reject' 
	WHERE id = @ID
END;

-- READ THE APPLICATION BASED ON THE ADMIN STATUS --

ALTER PROCEDURE SPA_Officer
AS
BEGIN
	SELECT * FROM user_detail
	WHERE admin_status = 'approve' AND officer_status IS NULL
END;

EXEC SPA_Officer

-- UPDATE OFFICER STATUS IN THE TABLE --

ALTER PROCEDURE SPV_Officer 
	@ID INT,
	@AdminStatus VARCHAR(20),
	@MessageInfo VARCHAR(50)
AS
BEGIN
	UPDATE user_detail 
	SET officer_status = 'verified',admin_status = @AdminStatus,message_info = @MessageInfo
	WHERE id = @ID
END;

ALTER PROCEDURE SPR_Officer 
	@ID INT,
	@MessageInfo VARCHAR(50)
AS
BEGIN
	UPDATE user_detail 
	SET officer_status = 'reject',message_info = @MessageInfo 
	WHERE id = @ID
END;

-- GENERATE PASSPORT NUMBER FROM ADMIN PAGE--

ALTER PROCEDURE SPP_Admin 
	@ID INT,
	@PassportNumber VARCHAR(10)
AS
BEGIN
	UPDATE user_detail SET passport_number = @PassportNumber WHERE id = @ID
END;
