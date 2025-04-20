-- =============================================
-- Author:		<cgroup86>
-- Create date: <13/6/2024>
-- Description:	<Insert instructor to the data base>
-- =============================================
CREATE PROCEDURE SP_InsertInstructor
	-- Add the parameters for the stored procedure here
	-- @id int,
	@title varchar(200),
	@name varchar(100),
	@image varchar(255),
	@jobTitle varchar(200)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [Instructors] ([title], [name], [image], [jobTitle]) VALUES(@title, @name, @image, @jobTitle);
END
GO
