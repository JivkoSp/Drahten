Each class in this directory contains unit tests related to a HandleAsync(...) public method of command handler that has the same name 
but without the Tests suffix.
For example the class CreateArticleHandlerTests contains unit tests related to CreateArticleHandler and so on.
-------------------------
*** Special features for CreateArticleHandlerTests *** 
--------------------------------------------------------
About the GivenValidArticleId_Calls_Repository_On_Success(...) method:
------------
In the moment the configuration for _articleMockFactory in the ACT section is NOT needed becouse the tests of the article factory 
ensure correct creation of article instance and there is no need to explicitly tell to NSubstitute that when the method 
_articleMockFactory.Create(...) is called it needs to return article object that has the same data as the data being provided to the method. 
Additionally there is no need to tell NSubstitute that when _articleRepository.AddArticleAsync(...) is called it needs to be called with that 
specific article instance (this is already ensured by the unit tests of the article factory becouse the tests ensure that when the factory 
is called, it will produce the expected article instance and thus indirectly that in this case this instance will be passed to the 
_articleRepository.AddArticleAsync(...) method).
*** However *** - In the future there is no guarantee that the behavior of the CreateArticleHandler will stay the same and that the 
article instance will not be changed between its creation by the factory and the _articleRepository.AddArticleAsync(...) method call.
Becouse of this the configuration for _articleMockFactory in the ACT section is needed and additionaly the check 
await _articleRepository.Received(1).AddArticleAsync(article) in the ASSERT section guarantee that the same article instance will be 
passed to the repository.
--------------------------------------------------------
*** Special features for CreateArticleHandlerTests *** 
--------------------------------------------------------