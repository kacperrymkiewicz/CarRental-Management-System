# Car Rental Management System
This application is designed to streamline the process of renting cars, providing users with features such as car search, reservation, reservation browsing, login, and registration. The application was created for educational purposes.

## Frontend Stack
- Vuejs 3
- Pinia
- Pinia persistedstate plugin
- Axios
- Vue I18n
- Vue Toastification
- Vuelidate
- Vue Datepicker
- Bootstrap 5
- SASS
- Vite

## Backend Stack
- ASP.NET WebApi
- Entity Framework
- Automapper
- MySQL
- JWT

## Core features
- Fully responsive
- Support for multiple languages
- **User Roles:** The system supports multiple user roles including customer, employee, manager, accountant and administrator, each with specific permissions and access levels.
- **Reservation Management:** Customers can search for available cars, make reservations, and view their reservation history. Employees can view reservations, confirm or cancel them, ensuring smooth operation.
- **Fleet Management:** Administrators have the capability to add new cars and manage the existing fleet, including updating car details, equipment, and parameters.

## Setup
Get the code by either cloning this repository using git

```sh
git clone https://github.com/kacperrymkiewicz/CarRental-Management-System.git
```
### Backend setup
#### Go to directory
```sh
cd CarRental-WebApi
```
#### Database migration
```sh
dotnet ef database update
```

#### Compiles and hot-reloads for development
```sh
dotnet watch run
```

#### Compile for Production
```sh
dotnet publish -r linux-x64 --no-self-contained
```

### Frontend setup
#### Go to directory
```sh
cd CarRental-Frontend
```

#### Install packages
```sh
npm install
```

#### Compile and Hot-Reload for Development

```sh
npm run dev
```

#### Compile and Minify for Production

```sh
npm run build
```


## Views
### Home page
![GUI IMG](/Docs/customer-homepage-logged-in-en.png)

### Search for available cars
![GUI IMG](/Docs/customer-car-search-en.png)

### Customer car reservation modal
![GUI IMG](/Docs/customer-car-reservation-confirmation-en.png)

### Customer car details view
![GUI IMG](/Docs/customer-car-details-pl.png)

### Customer reservations view
![GUI IMG](/Docs/customer-my-reservations-pl.png)

### Customer profile view
![GUI IMG](/Docs/customer-profile-pl.png)

### Administrator fleet management view
![GUI IMG](/Docs/admin-fleet-management.png)

### Employee customers list view
![GUI IMG](/Docs/employee-clients-list.png)

### Employee reservations view
![GUI IMG](/Docs/employee-reservations.png)

### Login view
![GUI IMG](/Docs/customer-login-page-pl.png)

### Register view
![GUI IMG](/Docs/customer-register-pl.png)

### Edit profile view
![GUI IMG](/Docs/customer-edit-profile-pl.png)

### Edit password view
![GUI IMG](/Docs/customer-edit-password.png)