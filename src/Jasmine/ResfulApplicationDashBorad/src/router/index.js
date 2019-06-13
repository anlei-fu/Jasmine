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
          path: 'srv',
          component: () => import("@/components/Service")
        },

        {
          path: 'grp',
          component: () => import("@/components/Group")
        },

        {
          path: 'summary',
          component: () => import("@/components/Summary")
        },

        {
          path: 'sysapi',
          component: () => import("@/components/SystemService")
        },

        {
          path: 'abt',
          component: () => import("@/components/About")
        },

        {
          path: 'usermng',
          component: () => import("@/components/UserManager")
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
