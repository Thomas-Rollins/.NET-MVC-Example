# .NET-MVC-Example

  This web app will allow a user to view, add, edit, and delete Shows and genres. Currently a show can only have a single genre and trying to delete a genre which a show is listed as will fail due to a FK_CONSTRAINT.

  This app uses the default .NET MVC template with bootstrap. The Site.css has been modified refer to the inline comments for more details.

  Added Input validation (On Raiting) and DataTypes to support multi-line form text-fields by default. As well as Data field name aliases.


Enabled logins (Social and individual stand-alone).
Included Twitter, Google, Microsoft, and FaceBook OAuths. 
When a user is annonomus the authorized-only links (create/edit/delete) are hidden.
There is no 'admin' role. It is assumed all authenticated users are able to create/edit/delete.

Live Site: https://comp2007-assignment2-200344886.azurewebsites.net/
