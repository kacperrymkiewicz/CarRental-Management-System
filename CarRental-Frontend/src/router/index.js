import { createRouter, createWebHistory } from 'vue-router'

import HomeView from '@/views/HomeView.vue'
import LoginView from '@/views/LoginView.vue'
import SignUpView from '@/views/SignUpView.vue'
import ProfileView from '@/views/ProfileView.vue'
import ProfileEditView from '@/views/ProfileEditView.vue'
import ProfileEditPasswordView from '@/views/ProfileEditPasswordView.vue'
import CarBookingView from '@/views/CarBookingView.vue'
import CarBookingDetailsView from '@/views/CarBookingDetailsView.vue'
import UserRentals from '@/views/UserRentals.vue'

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

export default router;
