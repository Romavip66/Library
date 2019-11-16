# Library Management System
# Milestone Project by Bekeshov Rymzhan
This project is about library management system. It allows user to create their own library card and via this card they can take many books. And one book can be shared at the same time. All these relations are regulated in Queue controller. And each book has it's valid period. After the experation of it's time, it will be displayed in History controller.

# RELATIONS:
In total 7 Entities: 
MODELS:
1 - user (ID, USERNAME, PASSWORD, FULLNAME) 
2 - role (ID, ROLE NAME, USER ID)
3 - requests (ID, USER ID, COMMENT)
4 - books (ID, NAME, AUTHOR NAME)
5 - library card (ID, USER ID, VALIDATION DATE)
6 - queue (ID, USER ID, BOOK ID, VALIDATION DATES)
7 - history (ID, USER ID, BOOK ID)

RELATIONS:
USER - ROLE (ROLE ID)//ONE TO MANY
USER - LIBRARY CARD(USER ID)//ONE TO ONE
QUEUE - BOOKS - LIBRARY CARD(BOOK ID, LIBRARY CARD ID)//MANY TO MANY
HISTORY - BOOKS - LIBRARY CARD(LIBRARY CARD ID, BOOK ID)//MANY TO MANY
REQUESTS - USER (USER ID)//ONE TO MANY
