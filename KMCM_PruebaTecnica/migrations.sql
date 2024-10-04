IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [KMCM_Persons] (
    [KMCM_ID_PERSON] int NOT NULL IDENTITY,
    [KMCM_NAME] varchar(100) NOT NULL,
    [KMCM_LASTNAME] varchar(100) NOT NULL,
    [KMCM_ADDRESS] varchar(200) NOT NULL,
    [KMCM_PHONE] varchar(15) NOT NULL,
    [KMCM_BIRTHDATE] date NOT NULL,
    CONSTRAINT [PK__Persons__ID_PERSON] PRIMARY KEY ([KMCM_ID_PERSON])
);
GO

CREATE TABLE [KMCM_Users] (
    [KMCM_ID_USER] int NOT NULL IDENTITY,
    [KMCM_USERNAME] varchar(50) NOT NULL,
    [KMCM_PASSWORD] varchar(100) NOT NULL,
    [kmcm_person_id] int NOT NULL,
    CONSTRAINT [PK__Users__ID_USER] PRIMARY KEY ([KMCM_ID_USER]),
    CONSTRAINT [FK_User_Person] FOREIGN KEY ([kmcm_person_id]) REFERENCES [KMCM_Persons] ([KMCM_ID_PERSON]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_KMCM_Users_kmcm_person_id] ON [KMCM_Users] ([kmcm_person_id]);
GO

-- Insertar la persona
INSERT INTO [KMCM_Persons] (KMCM_NAME, KMCM_LASTNAME, KMCM_ADDRESS, KMCM_PHONE, KMCM_BIRTHDATE)
VALUES ('ADMIN', 'PRUEBA', 'PRUEBA_DIRECCION', '0999999999', '2000-01-01');
GO

-- Insertar el usuario
INSERT INTO [KMCM_Users] (KMCM_USERNAME, KMCM_PASSWORD, kmcm_person_id)
VALUES ('admin', '38GLTXFinSFH3yOiXwZdyw==', SCOPE_IDENTITY());
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241004071114_thirdMigration', N'7.0.20');
GO


COMMIT;
GO

