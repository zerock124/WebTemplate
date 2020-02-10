import Vue from 'vue';
import BootstrapVue from 'bootstrap-vue';
import FontHomeCreateManagement from './FontHomeCreateManagement';
import  ToggleButton  from 'vue-js-toggle-button'

Vue.use(ToggleButton);
Vue.use(BootstrapVue);

new Vue({
    render: (h => h(FontHomeCreateManagement))
}).$mount('#v_app');