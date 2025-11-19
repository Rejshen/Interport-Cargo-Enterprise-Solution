#  InterportCargo – Freight Quotation Management System  
*A .NET Razor Pages prototype developed for QUT IAB251 Assignment 2*

---

##  Overview  
**InterportCargo** is a web-based freight quotation management prototype that simulates how customers and employees interact in a logistics company.  
It allows users to register, log in, request freight quotations, and manage those quotations through a simplified workflow using **Entity Framework Core** with a **SQLite** database.

This project was developed as part of **QUT IAB251 – Enterprise Systems** to demonstrate system design, data flow, and integration between front-end and back-end components.

---

##  Core Features  

###  User Management  
- **Customer Registration** – new customers can register by entering personal and contact details.  
- **Employee Registration** – new employees can register with defined roles (e.g., Quotation Officer).  
- **Login & Logout** – both customers and employees can securely access system functions.  

###  Quotation Management  
- **Quotation Request (Customer)** – customers can submit quotation requests with source, destination, container count, and job nature (Import/Export, Packing, Quarantine).  
- **Officer Dashboard (Quotation Officer)** – officers can view, accept, or reject requests.  
- **Prepare Quotation** – officers can calculate rates, apply discounts, and finalize quotes.  
- **Rate Schedule Integration** – quotation calculations reference base rates from a predefined schedule.  
- **Discount Rules** – automatic discounts based on container count and job conditions.  

###  Messaging & Notifications  
- Officers can send messages to customers when requests are rejected.  
- Customers can view messages, mark them as read, and track quotation updates.  

---

##  Technologies Used  
- **ASP.NET Core Razor Pages (C#)**  
- **Entity Framework Core (SQLite Database)**  
- **Bootstrap 5** for responsive UI  
- **LINQ** and **EF Migrations** for data management  
- **Session Management** for authentication  

---

##  Database Entities  
- `Customer`  
- `Employee`  
- `QuotationRequest`  
- `Quotation`  
- `RateSchedule`  
- `CustomerMessage`  

Each entity supports validation, relationships, and data persistence through EF Core.

---

##  How to Run  

1. Clone the repository  
   ```bash
   git clone https://github.com/<your-username>/InterportCargo.git
   cd InterportCargo
