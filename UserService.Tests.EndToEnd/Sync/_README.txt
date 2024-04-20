This directory contains unit tests for the api endpoints in the presentation layer.
--------------------------------------------------------
				**** PostgreSQL ****
--------------------------------------------------------
PostgreSQL preserves microsecond-level precision when storing a DateTimeOffset value but does not retain 
nanosecond-level precision. As a result, any nanoseconds present in the original DateTimeOffset value would be rounded or truncated 
to microseconds when stored in the database. Therefore, there might be instances where the time of two objects that were previously 
initialized with equal times is not exactly equal.
Due to this reason, the time comparison is done with a tolerance of 1 millisecond.
--------------------------------------------------------
				**** PostgreSQL ****
--------------------------------------------------------


--------------------------------------------------------
*** Special features for RegisterBannedUserTests *** 
--------------------------------------------------------
About the Register_BannedUser_Endpoint_Should_Add_BannedUser_With_Given_IssuerUserId_And_ReceiverUserId_To_The_Database(...) method:
------------
The time comaprison of the objects banUserCommand and issuedBanByUserDto is NOT done like this: 
issuedBanByUserDto.DateTime.ShouldBe(banUserCommand.DateTime);
This is because of the reasons mentioned in the section - **** PostgreSQL **** in this document.
--------------------------------------------------------
*** Special features for RegisterBannedUserTests *** 
--------------------------------------------------------