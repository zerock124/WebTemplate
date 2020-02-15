import Vue from 'vue';
import { BvToastOptions } from 'bootstrap-vue';
import { ServiceItemViewModel } from './model';

const ServiceItemEvent: EventVue = new Vue();
export default ServiceItemEvent;

interface EventVue extends Vue {
    $emit: CustomEvent;
}

interface CustomEvent {
    (event: string, ...args: any[]);
	/**
	* AddNewToastr
    * 
	* 生成 組件 created
	* 結束 組件 beforeDistroy
	*
	* @param message 內容
	* @param setting 設定值
	*
	*/
    (event: 'AddNewToastr', message: string, setting: BvToastOptions);

    /**
    * OpenSetDriverWithVehicleModal
    * 
    * 生成 組件 created
    * 結束 組件 beforeDistroy
    *
    */
    (event: 'EmitServiceItem', serviceItem: ServiceItemViewModel);

}