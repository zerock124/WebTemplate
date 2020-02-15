import Vue from 'vue';
import BootstrapVue from 'bootstrap-vue';
import LatestNewsCreateManagement from './LatestNewsCreateManagement';
import  ToggleButton  from 'vue-js-toggle-button'

Vue.use(ToggleButton);
Vue.use(BootstrapVue);

new Vue({
    render: (h => h(LatestNewsCreateManagement))
}).$mount('#v_app');