import Vue from 'vue';
import BootstrapVue from 'bootstrap-vue';
import ContactManagement from './ContactManagement';
import ToggleButton from 'vue-js-toggle-button'

Vue.use(ToggleButton);
Vue.use(BootstrapVue);

new Vue({
    render: (h => h(ContactManagement))
}).$mount('#v_app');