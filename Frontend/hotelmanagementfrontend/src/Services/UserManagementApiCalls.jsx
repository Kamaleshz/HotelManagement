import axios from "axios";
import UserManagementConstants from "../Utilities/UserModuleConstants";

class UserManagementService {
    http = axios.create({
        baseURL : UserManagementConstants.BaseURL
    })

    async login(obj) {
            let responce = await this.http.post(UserManagementConstants.Login, obj);
            console.log(responce.data);
            return responce.data;
        }
}

export default new UserManagementService();