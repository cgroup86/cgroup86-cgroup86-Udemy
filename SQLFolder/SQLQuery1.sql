Create Table [Instructors] (
	[id] int identity(1, 1) PRIMARY KEY,
	[title] nvarchar(200),
	[name] nvarchar(200),
	[image] nvarchar(300),
	[jobTitle] nvarchar(200)
)

Create Table [Courses] (
	[id] int identity(1, 1) PRIMARY KEY,
	[title] nvarchar(200),
	[url] nvarchar(255),
	[rating] float,
	[numberOfReviews] int,
	[instructorId] int references [Instructors](id),
	[imageReference] nvarchar(300),
	[duration] float,
	[lastUpdate] char(10)
)

Create Table [Users] (
	[id] int identity(0,1) PRIMARY KEY ,
	[name] nvarchar(200),
	[email] nvarchar(200),
	[password] nvarchar(200),
	[isAdmin] bit,
	[isActive] bit
)

Create Table [UserCourse] (
	UserId int,
	CourseId int,
	constraint fk1 foreign key([UserId]) references [Users](id) on delete cascade,
	constraint fk2 foreign key([CourseId]) references [Courses](id) on delete cascade,
	primary key(UserId, CourseId)
)