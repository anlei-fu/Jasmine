// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import router from './router'
import iView from 'iview'
import store from './Utils/store'
import cookies from 'vue-cookies'
import 'iview/dist/styles/iview.css'

Vue.config.productionTip = false

/* config iview */
Vue.use(iView)
Vue.use(cookies)

/* eslint-disable no-new */
var vue = new Vue({
  el: '#app',
  router,
  store,
  components: { App },
  template: '<App/>',
  methods: {
    mapGroups(groups) {
      // mapping group
      var gmap = new Map()
      groups.foreach(group => {
        // mapping service
        var smap = new Map();
        group.foreach(service => {
          smap.set(service.Name, service);
        });
        gmap.set(group.Name, smap);
      });

      return gmap;
    },

    mapGroup(group) {
      var smap = new Map();
      group.foreach(service => {
        smap.set(service.Name, service);
      });

      return smap;
    }
  }
})

/* intercept if unsignined */
router.beforeEach((to, from, next) => {
  // loading bar start
  iView.LoadingBar.start();

  // can not use this to represent vue
  console.log(vue);
  console.log(from.path + ' want to ' + to.path);
  /* root path ignore */
  if (to.path === '/login') {
    if (vue.$cookies.get("token")) {
      var groups = vue.$visitor.getData("/getall");
      if (groups) {
        vue.$store.commit('refreshAll', vue.mapGroups(groups));
        vue.$store.state.isSignIned = true;
        vue.route.push({ path: '/main' });
      } else {
        next();
      }
    } else {
      next();
    }
  } else if (from.path !== '/') {
    console.log(vue.$store.state.isSignIned);
    // un signined and not go login  ,redirect to login
    if (!vue.$store.state.isSignIned) {
      router.push({ path: '/login' })
    } else {
      next();
    }
  } else {
    next();
  }
});

/*  intercept route loaded */
router.afterEach(route => {
  iView.LoadingBar.finish();
});
