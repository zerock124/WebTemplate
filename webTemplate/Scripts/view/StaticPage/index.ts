import Vue from 'vue';
import BootstrapVue from 'bootstrap-vue';
import StaticPageManagement from './StaticPageManagement';

Vue.use(BootstrapVue);

new Vue({
    render: (h => h(StaticPageManagement))
}).$mount('#v_app');