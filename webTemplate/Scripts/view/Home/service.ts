import { IHomeService, HomeViewModel } from './model'
import { AjaxReturn } from '../Share/PublicFunction';



class HomeService implements IHomeService {

    GetHomeDate(): JQuery.jqXHR<ResWithPaginationViewModel<HomeViewModel>> {
        const setting: JQueryAjaxSettings = {
            url: '/Home/GetHomeDate',
            type: 'GET',
        }
        return AjaxReturn(setting);
    }
}

const home_service: IHomeService = new HomeService();
export default home_service;