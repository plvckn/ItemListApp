# ItemListApp

Item List App allows the user to create nested lists in a hierarchical tree structure. Each list has its name and value (automatically
generated). Using app's interface user can create new lists, sort (ascending/descending order) or sum the item values of any chosen
list.

Item list structure looks like this:


![Alt text](https://i.imgur.com/fj8hOZ1.png)

Item names go in an increasing alphabetical order, with inner list inheriting its parent name and adding to it, making every
item unique and easily identifiable.

Screenshort of user interface:
![Alt text](https://i.imgur.com/0yFft9N.png)

To run this application, clone the repo and make sure WPF application (ListExe) is set to be a start-up project for the solution and not
the ItemListLibrary. Also make sure to add a reference in 'ListExe' to class 'ItemListLibrary' if it is not there already.
