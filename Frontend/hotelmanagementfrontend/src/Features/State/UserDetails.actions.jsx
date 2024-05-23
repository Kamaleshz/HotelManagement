import { Set_User_Details } from "./UserDetails.types";

export const setUserDetails = (userDetails) => ({
    type : Set_User_Details,
    payload : userDetails,
});