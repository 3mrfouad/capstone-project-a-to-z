import { applyMiddleware, createStore } from "redux";
import { composeWithDevTools } from "redux-devtools-extension";
import thunk from "redux-thunk";
import rootReducer from "./reducers/index";

const configureStore = () => {
  const middleware = [thunk];
  const middlewareEnhancer = applyMiddleware(...middleware);
  const store = createStore(
    rootReducer,
    composeWithDevTools(middlewareEnhancer)
  );
  return store;
};

export default configureStore;
