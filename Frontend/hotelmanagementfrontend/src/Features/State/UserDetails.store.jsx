import { createStore, combineReducers } from "redux";
import UserDetailsReducer from "./UserDetails.Reducer";


const store = createStore(UserDetailsReducer);
export default store;
