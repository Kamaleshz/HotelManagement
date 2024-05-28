import { createStore } from "redux";
import { persistStore, persistReducer } from "redux-persist";
import storage from "redux-persist/lib/storage";
import UserDetailsReducer from "./UserDetails.reducer";

const persistConfig = {
    key: "root",
    storage,
};

const persistedReducer = persistReducer(persistConfig, UserDetailsReducer);

const store = createStore(persistedReducer);
const persistor = persistStore(store);

export { store, persistor };
