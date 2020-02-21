import { IContactService, ContactViewModel, SearchModel } from './model'
import { AjaxReturn } from '../Share/PublicFunction';

class ContactService implements IContactService {
    GetContactList(SearchModel: SearchModel, sendPagination: SendPaginationModel): JQuery.jqXHR<ResWithPaginationViewModel<ContactViewModel[]>> {
        const setting: JQueryAjaxSettings = {
            url: '/Contact/GetContactList',
            type: 'POST',
            data: {
                ...SearchModel,
                ...sendPagination
            }
        }
        return AjaxReturn(setting);
    }

    GetEditContactItem(ContactId: number): JQuery.jqXHR<ResponseViewModel> {
        const setting: JQueryAjaxSettings = {
            url: `/Contact/GetEditContactItem?ContactId=${ContactId}`,
            type: 'GET',
        };

        return AjaxReturn(setting);
    }

    EditContactItem(model: ContactViewModel): JQuery.jqXHR<ResponseViewModel> {
        const setting: JQueryAjaxSettings = {
            url: `/Contact/EditContactItem`,
            type: 'POST',
            data: model
        };

        return AjaxReturn(setting);
    }

    DeleteContact(ContactId: number): JQuery.jqXHR<ResponseViewModel> {
        const setting: JQueryAjaxSettings = {
            url: `/Contact/DeleteContact`,
            type: 'POST',
            data: { ContactId }
        };

        return AjaxReturn(setting);
    }
}

const contact_service: IContactService = new ContactService();
export default contact_service;