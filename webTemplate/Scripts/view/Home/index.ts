import Vue from 'vue';
import BootstrapVue from 'bootstrap-vue';
import HomeManagement from './HomeManagement';

Vue.use(BootstrapVue);

new Vue({
    render: (h => h(HomeManagement))
}).$mount('#v_app');