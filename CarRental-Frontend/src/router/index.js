import { createRouter, createWebHistory } from 'vue-router'
import { useToast } from 'vue-toastification';

import HomeView from '@/views/HomeView.vue'
import LoginView from '@/views/LoginView.vue'
import SignUpView from '@/views/SignUpView.vue'
import ProfileView from '@/views/ProfileView.vue'
import ProfileEditView from '@/views/ProfileEditView.vue'
import ProfileEditPasswordView from '@/views/ProfileEditPasswordView.vue'
import CarBookingView from '@/views/CarBookingView.vue'
import CarBookingDetailsView from '@/views/CarBookingDetailsView.vue'
import UserRentals from '@/views/UserRentals.vue'
import RentalsListView from '@/views/employee/RentalsListView.vue'
import CustomersListView from '@/views/employee/CustomersListView.vue'
import CarsListView from '@/views/employee/CarsListView.vue'
import NewCarView from '@/views/employee/NewCarView.vue'

const routes = [
  {
    path: '/',
    name: 'home',
    component: HomeView
  },
  {
    path: '/logowanie',
    name: 'login',
    component: LoginView
  },
  {
    path: '/rejestracja',
    name: 'signup',
    component: SignUpView
  },
  {
    path: '/profil',
    children: [
      {
        path: '',
        name: 'profile',
        component: ProfileView,
      },
      {
        path: 'edycja-profilu',
        name: 'profile-edit',
        component: ProfileEditView
      },
      {
        path: 'edycja-hasla',
        name: 'profile-edit-password',
        component: ProfileEditPasswordView
      },
    ]
  },
  {
    path: '/rezerwacja',
    name: 'booking',
    component: CarBookingView
  },
  {
    path: '/rezerwacje',
    name: 'reservations',
    component: UserRentals
  },
  {
    path: '/samochody',
    children: [
      {
        path: '',
        name: 'cars',
        component: HomeView,
      },
      {
        path: ':id/:slug?',
        name: 'car',
        component: CarBookingDetailsView
      }
    ]
  },
  {
    path: '/panel',
    meta: {
      requiresAuth: true,
    },
    children: [
      {
        path: 'rezerwacje',
        name: 'panel-booking',
        component: RentalsListView
      },
      {
        path: 'klienci',
        name: 'panel-customers',
        component: CustomersListView
      },
      {
        path: 'samochody',
        children: [
          {
            path: '',
            name: 'panel-cars',
            component: CarsListView
          },
          {
            path: 'dodaj',
            name: 'panel-cars-add',
            component: NewCarView
          }
        ]
      },
    ]
  },
  // {
  //   path: '/about',
  //   name: 'about',
  //   // route level code-splitting
  //   // this generates a separate chunk (About.[hash].js) for this route
  //   // which is lazy-loaded when the route is visited.
  //   component: () => import('../views/AboutView.vue')
  // }
];

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes,
  scrollBehavior() {
    return { top: 0, left: 0, /*behavior: "instant"*/ }
  }
});

router.beforeEach((to) => {
  if(to.meta.requiresAuth && !localStorage.getItem('user')){
    const toast = useToast();
    toast.error("Ta akcja wymaga autoryzacji");
    return  { name: "login" } ;
  }
  if(to.meta.requiresEmployeePerms && localStorage.getItem('role') != 'Employee'){
    const toast = useToast();
    toast.error("Ta akcja wymaga autoryzacji. Poziom uprawnień: Pracownik");
    return  { name: "login" } ;
  }
  if(to.meta.requiresAdministratorPerms && localStorage.getItem('role') != 'Administrator'){
    const toast = useToast();
    toast.error("Ta akcja wymaga autoryzacji. Poziom uprawnień: Administrator");
    return  { name: "login" } ;
  }
  return true
})

export default router;
