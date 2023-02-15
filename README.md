# SchoolApp Automated Attendance System 
Imagine a school where students and staffs dont need to write attendace for students in school well here is a solution. 

A Student has a unique code generated to them when they register for the school and they have a card given to them with their student number embedded in a QRCODE

# How It works
A student QRCode is generated from the Barcode endpoint

At the start of the day once a student comes to the school premises the student checks in with their Card by Tapping their Card on a QRcode Reader

it Gets the student Number and Matches them to existing student record in the database then Record an Attendance for the day for that child which includes time in and 
day of the week. 

At the close of the day or emergency a student can also check out by tapping their card on the QRCode Reader and it checks them out for the day. 

# Parent Watcher

As a parent of any child/children in the school you can monitor the attendance of your ward in the school as a notification system is placed in the api to give you live

update on check in and check out of their wards.

