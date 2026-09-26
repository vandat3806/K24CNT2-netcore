IF DB_ID(N'TvcLesson10EFDb') IS NULL
BEGIN
    EXEC(N'CREATE DATABASE [TvcLesson10EFDb]');
END
GO

USE [TvcLesson10EFDb];
GO

IF OBJECT_ID(N'dbo.NvdMember', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.NvdMember
    (
        Id BIGINT IDENTITY(1,1) NOT NULL,
        NvdUserName VARCHAR(20) NOT NULL,
        NvdPassword VARCHAR(200) NOT NULL,
        NvdFullName NVARCHAR(100) NOT NULL,
        NvdEmail VARCHAR(100) NOT NULL,
        NvdPhone VARCHAR(15) NULL,
        NvdStatus BIT NOT NULL CONSTRAINT DF_NvdMember_Status DEFAULT (1),
        NvdCreatedAt DATETIME2 NOT NULL CONSTRAINT DF_NvdMember_CreatedAt DEFAULT (SYSUTCDATETIME()),
        CONSTRAINT PK_NvdMember PRIMARY KEY (Id),
        CONSTRAINT UQ_NvdMember_UserName UNIQUE (NvdUserName),
        CONSTRAINT UQ_NvdMember_Email UNIQUE (NvdEmail)
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM dbo.NvdMember)
BEGIN
    INSERT INTO dbo.NvdMember
        (NvdUserName, NvdPassword, NvdFullName, NvdEmail, NvdPhone, NvdStatus)
    VALUES
        ('nguyenvandat', 'DU_LIEU_MAU_KHONG_DUNG_DANG_NHAP', N'Nguyễn Văn Đạt', 'dat.nguyen@ntu.edu.vn', '0988000021', 1),
        ('thanhdat', 'DU_LIEU_MAU_KHONG_DUNG_DANG_NHAP', N'Nguyễn Trần Thành Đạt', 'thanhdat@ntu.edu.vn', '0988000020', 1),
        ('xuanbac', 'DU_LIEU_MAU_KHONG_DUNG_DANG_NHAP', N'Nguyễn Xuân Bắc', 'xuanbac@ntu.edu.vn', '0988000031', 1),
        ('xuantruong', 'DU_LIEU_MAU_KHONG_DUNG_DANG_NHAP', N'Nguyễn Xuân Trường', 'xuantruong@ntu.edu.vn', '0988000041', 0),
        ('manhtung', 'DU_LIEU_MAU_KHONG_DUNG_DANG_NHAP', N'Nguyễn Mạnh Tùng', 'manhtung@ntu.edu.vn', '0988000051', 1);
END
GO

SELECT Id, NvdUserName, NvdFullName, NvdEmail, NvdPhone, NvdStatus, NvdCreatedAt
FROM dbo.NvdMember
ORDER BY Id;
GO
