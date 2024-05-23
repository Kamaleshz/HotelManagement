import { Set_User_Details } from "./UserDetails.types";

const intialState = {
    userDetails:"",
};

const UserDetailsReducer = (state=intialState, action) => {
    switch(action.type)
    {
        case Set_User_Details:            
            return{
                ...state,
                userDetails : action.payload,
            }

        default:
            return{
                state,
            };
    }
}

export default UserDetailsReducer;