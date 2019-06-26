import Vue from 'vue'
import Router from 'vue-router'

Vue.use(Router);

export default new Router({
  routes: [

    /* main page */
    {
      path: '/main',
      component: () => import('@/components/Main'),

      /* main sub pages */
      children: [

        {
          path: 'service',
          component: () => import("@/components/Service")
        },

        {
          path: 'group',
          component: () => import("@/components/Group")
        },

        {
          path: 'summary',
          component: () => import("@/components/Summary")
        },

        {
          path: 'systemapi',
          component: () => import("@/components/SystemService")
        },

        {
          path: 'about',
          component: () => import("@/components/About")
        },

        {
          path: 'usermanage',
          component: () => import("@/components/UserManager")
        },
        {
          path: 'debug',
          component: () => import('@/components/RestfulClient')
        }

      ]
    },

    /* login page */
    {
      path: '/',
      component: () => import("@/components/Login")
    },
     /* login page */
     {
      path: '/login',
      component: () => import("@/components/Login")
    }
  ]
});
