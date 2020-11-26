import { combineReducers } from "redux";
import { cohortSummaryInstructorReducer } from "./instructorReducer";

const rootReducers = combineReducers({
  cohortSummaryInstructor: cohortSummaryInstructorReducer,
});

export default rootReducers;
