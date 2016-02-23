Feature: Login
	I want to be sure that i can login and logout from app

@Login
Scenario: GWD_Login
	Given I have entered URL: http://localhost:6487/
	And I have clicked loginLink on Home Page
	And I have typed kz into the UserName on Login Page
	And I have typed kzkzkz into the Password on Login Page
	When I click LoginSubmit on Login Page
	Then the UserName should be kz on Home Page


@Login
Scenario: GWD_LogOut
	Given I have logged in
	When I have clicked LogoutButton on Home Page
	Then the UserName should not be displayed on Home Page





