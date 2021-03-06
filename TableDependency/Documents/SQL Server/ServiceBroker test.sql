USE [TableDependencyDB]
GO

-- ***************************************************************************************************
--	Creation
-- ***************************************************************************************************
CREATE MESSAGE TYPE [TestTable/4649b56e-60d6-47ea-afdb-7266ea0f407d/Deleted/id] VALIDATION = NONE
CREATE MESSAGE TYPE [TestTable/4649b56e-60d6-47ea-afdb-7266ea0f407d/Deleted/name] VALIDATION = NONE
CREATE MESSAGE TYPE [TestTable/4649b56e-60d6-47ea-afdb-7266ea0f407d/Deleted/surname] VALIDATION = NONE
CREATE MESSAGE TYPE [TestTable/4649b56e-60d6-47ea-afdb-7266ea0f407d/Deleted/long description] VALIDATION = NONE


CREATE CONTRACT [TestTable_4649b56e-60d6-47ea-afdb-7266ea0f407d]
( 
	[TestTable/4649b56e-60d6-47ea-afdb-7266ea0f407d/Deleted/id] SENT BY INITIATOR,
	[TestTable/4649b56e-60d6-47ea-afdb-7266ea0f407d/Deleted/name] SENT BY INITIATOR,
    [TestTable/4649b56e-60d6-47ea-afdb-7266ea0f407d/Deleted/surname] SENT BY INITIATOR,
	[TestTable/4649b56e-60d6-47ea-afdb-7266ea0f407d/Deleted/long description] SENT BY INITIATOR
)


CREATE QUEUE [TestTable_4649b56e-60d6-47ea-afdb-7266ea0f407d] 
WITH STATUS = ON, RETENTION = OFF, POISON_MESSAGE_HANDLING (STATUS = OFF)

CREATE SERVICE [TestTable_4649b56e-60d6-47ea-afdb-7266ea0f407d] ON QUEUE [TestTable_4649b56e-60d6-47ea-afdb-7266ea0f407d] ([TestTable_4649b56e-60d6-47ea-afdb-7266ea0f407d]);


-- ***************************************************************************************************
-- Start a conversation
-- ***************************************************************************************************
DECLARE @conversation uniqueidentifier
DECLARE @conversation_group_id uniqueidentifier

SET @conversation_group_id = NEWID();

BEGIN DIALOG @conversation
FROM SERVICE [TestTable_4649b56e-60d6-47ea-afdb-7266ea0f407d]
TO SERVICE 'TestTable_4649b56e-60d6-47ea-afdb-7266ea0f407d'    
ON CONTRACT [TestTable_4649b56e-60d6-47ea-afdb-7266ea0f407d]
WITH ENCRYPTION=OFF, RELATED_CONVERSATION_GROUP = @conversation_group_id;

--- Send the message
SEND ON CONVERSATION @conversation MESSAGE TYPE [TestTable/4649b56e-60d6-47ea-afdb-7266ea0f407d/Deleted/id] (CONVERT(NVARCHAR(MAX), 1702));
SEND ON CONVERSATION @conversation MESSAGE TYPE [TestTable/4649b56e-60d6-47ea-afdb-7266ea0f407d/Deleted/id] (CONVERT(NVARCHAR(MAX), GETDATE(), 121));
SEND ON CONVERSATION @conversation MESSAGE TYPE [TestTable/4649b56e-60d6-47ea-afdb-7266ea0f407d/Deleted/name] (CONVERT(NVARCHAR(MAX), 'Nonna Velia'));
SEND ON CONVERSATION @conversation MESSAGE TYPE [TestTable/4649b56e-60d6-47ea-afdb-7266ea0f407d/Deleted/long description] (CONVERT(NVARCHAR(MAX),'<?xml version="1.0" encoding="UTF-8"?><catalog><book id="bk101"><author>Gambardella, Matthew</author><title>XML Developer''s Guide</title><genre>Computer</genre><price>44.95</price><publish_date>2000-10-01</publish_date><description>An in-depth look at creating applications with XML.</description></book><book id="bk102"><author>Ralls, Kim</author><title>Midnight Rain</title><genre>Fantasy</genre><price>5.95</price><publish_date>2000-12-16</publish_date><description>A former architect battles corporate zombies, an evil sorceress, and her own childhood to become queen of the world.</description></book><book id="bk103"><author>Corets, Eva</author><title>Maeve Ascendant</title><genre>Fantasy</genre><price>5.95</price><publish_date>2000-11-17</publish_date><description>After the collapse of a nanotechnology society in England, the young survivors lay the foundation for a new society.</description></book><book id="bk104"><author>Corets, Eva</author><title>Oberon''s Legacy</title><genre>Fantasy</genre><price>5.95</price><publish_date>2001-03-10</publish_date><description>In post-apocalypse England, the mysterious agent known only as Oberon helps to create a new life for the inhabitants of London. Sequel to Maeve Ascendant.</description></book><book id="bk105"><author>Corets, Eva</author><title>The Sundered Grail</title><genre>Fantasy</genre><price>5.95</price><publish_date>2001-09-10</publish_date><description>The two daughters of Maeve, half-sisters, battle one another for control of England. Sequel to Oberon''s Legacy.</description></book><book id="bk106"><author>Randall, Cynthia</author><title>Lover Birds</title><genre>Romance</genre><price>4.95</price><publish_date>2000-09-02</publish_date><description>When Carla meets Paul at an ornithology conference, tempers fly as feathers get ruffled.</description></book><book id="bk107"><author>Thurman, Paula</author><title>Splish Splash</title><genre>Romance</genre><price>4.95</price><publish_date>2000-11-02</publish_date><description>A deep sea diver finds true love twenty thousand leagues beneath the sea.</description></book><book id="bk108"><author>Knorr, Stefan</author><title>Creepy Crawlies</title><genre>Horror</genre><price>4.95</price><publish_date>2000-12-06</publish_date><description>An anthology of horror stories about roaches, centipedes, scorpions  and other insects.</description></book><book id="bk109"><author>Kress, Peter</author><title>Paradox Lost</title><genre>Science Fiction</genre><price>6.95</price><publish_date>2000-11-02</publish_date><description>After an inadvertant trip through a Heisenberg Uncertainty Device, James Salway discovers the problems of being quantum.</description></book><book id="bk110"><author>O''Brien, Tim</author><title>Microsoft .NET: The Programming Bible</title><genre>Computer</genre><price>36.95</price><publish_date>2000-12-09</publish_date><description>Microsoft''s .NET initiative is explored in detail in this deep programmer''s reference.</description></book><book id="bk111"><author>O''Brien, Tim</author><title>MSXML3: A Comprehensive Guide</title><genre>Computer</genre><price>36.95</price><publish_date>2000-12-01</publish_date><description>The Microsoft MSXML3 parser is covered in detail, with attention to XML DOM interfaces, XSLT processing, SAX and more.</description></book><book id="bk112"><author>Galos, Mike</author><title>Visual Studio 7: A Comprehensive Guide</title><genre>Computer</genre><price>49.95</price><publish_date>2001-04-16</publish_date><description>Microsoft Visual Studio 7 is explored in depth, looking at how Visual Basic, Visual C++, C#, and ASP+ are integrated into a comprehensive development environment.</description></book></catalog>'));

END CONVERSATION @conversation WITH CLEANUP;



-- ***************************************************************************************************
-- reading
-- ***************************************************************************************************
SELECT * FROM sys.conversation_endpoints order by security_timestamp desc
SELECT * FROM [dbo].[dbo_FlightBookings_64d4fa33-7d3d-406b-be92-998af4a3197e] WITH (NOLOCK)

-- Content of the message: varbinary(MAX)
WAITFOR (RECEIVE TOP(1) CONVERT(NVARCHAR(MAX), message_body) as 'message_body', * FROM [TestTable_4649b56e-60d6-47ea-afdb-7266ea0f407d]);



DECLARE @TestString VARCHAR(100) = 'Test String' ;

SELECT  @TestString AS [Raw],
        CONVERT(VARBINARY(100), @TestString) AS ToBinary,
        CONVERT(VARCHAR(100), CONVERT(VARBINARY(100), @TestString)) AS StringFromBinaryFromString ;



