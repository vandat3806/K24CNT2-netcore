-- Bai thi ASP.NET Core MVC - Nguyen Van Dat - 2410900021
-- He quan tri: Microsoft SQL Server / LocalDB (chay bang SSMS hoac SQL Server Object Explorer)
-- Goi y: Hvt = Nguyen Van Dat => Nvd. File co the chay lai ma khong xoa du lieu cu.

IF DB_ID(N'NvdEmployee_2410900021_Db') IS NULL
BEGIN
    CREATE DATABASE [NvdEmployee_2410900021_Db];
END;
GO

USE [NvdEmployee_2410900021_Db];
GO

IF OBJECT_ID(N'dbo.NvdEmployee', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.NvdEmployee
    (
        Id INT IDENTITY(1, 1) NOT NULL CONSTRAINT PK_NvdEmployee PRIMARY KEY,
        NvdName NVARCHAR(100) NOT NULL,
        NvdGender NVARCHAR(10) NULL,
        NvdBirthDay DATE NULL,
        NvdEmail NVARCHAR(255) NULL,
        NvdPhone VARCHAR(15) NULL,
        NvdActive BIT NOT NULL CONSTRAINT DF_NvdEmployee_NvdActive DEFAULT (1),
        CONSTRAINT CK_NvdEmployee_NvdGender CHECK (NvdGender IS NULL OR NvdGender IN (N'Nam', N'Nữ', N'Khác'))
    );
END;
GO

IF OBJECT_ID(N'dbo.NvdStudent', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.NvdStudent
    (
        Id INT IDENTITY(1, 1) NOT NULL CONSTRAINT PK_NvdStudent PRIMARY KEY,
        NvdStudentCode VARCHAR(20) NOT NULL,
        NvdName NVARCHAR(100) NOT NULL,
        NvdGender NVARCHAR(10) NULL,
        NvdBirthDay DATE NULL,
        NvdEmail NVARCHAR(255) NULL,
        NvdPhone VARCHAR(15) NULL,
        NvdClass VARCHAR(30) NOT NULL,
        NvdActive BIT NOT NULL CONSTRAINT DF_NvdStudent_NvdActive DEFAULT (1),
        CONSTRAINT UQ_NvdStudent_NvdStudentCode UNIQUE (NvdStudentCode),
        CONSTRAINT CK_NvdStudent_NvdGender CHECK (NvdGender IS NULL OR NvdGender IN (N'Nam', N'Nữ', N'Khác'))
    );
END;
GO

IF NOT EXISTS (SELECT 1 FROM dbo.NvdStudent WHERE NvdStudentCode = '2410900021')
BEGIN
    INSERT INTO dbo.NvdStudent (NvdStudentCode, NvdName, NvdClass, NvdActive)
    VALUES ('2410900021', N'Nguyễn Văn Đạt', 'K24CNT2', 1);
END;
GO

INSERT INTO dbo.NvdStudent (NvdStudentCode, NvdName, NvdGender, NvdClass, NvdActive)
SELECT demo.StudentCode, demo.FullName, demo.Gender, demo.ClassName, demo.Active
FROM (VALUES
    ('9999991001', N'Trần Minh An', N'Nam', 'K24CNT1', CAST(1 AS BIT)),
    ('9999991002', N'Lê Thu Hà', N'Nữ', 'K24CNT2', CAST(1 AS BIT)),
    ('9999991003', N'Phạm Hải Bình', N'Nam', 'K24CNT3', CAST(1 AS BIT)),
    ('9999991004', N'Đỗ Ngọc Mai', N'Nữ', 'K24CNT2', CAST(1 AS BIT)),
    ('9999991005', N'Vũ Hoàng Long', N'Nam', 'K24CNT1', CAST(0 AS BIT)),
    ('9999991006', N'Hoàng Quỳnh Anh',N'Nữ', 'K24CNT3', CAST(1 AS BIT))
) AS demo(StudentCode, FullName, Gender, ClassName, Active)
WHERE NOT EXISTS
    (SELECT 1 FROM dbo.NvdStudent AS s WHERE s.NvdStudentCode = demo.StudentCode);
GO

SELECT * FROM dbo.NvdEmployee;
SELECT * FROM dbo.NvdStudent;
GO
