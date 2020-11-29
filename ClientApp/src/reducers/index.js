import { combineReducers } from "redux";
import { cohortSummaryInstructorReducer } from "./instructorReducer";
import {
  courseSummaryStudentReducer,
  homeworkSummaryStudentReducer,
  homeworkStudentReducer,
  createTimeSheetStudentReducer,
} from "./studentReducer";

const rootReducers = combineReducers({
  cohortSummaryInstructor: cohortSummaryInstructorReducer,
  courseSummaryStudent: courseSummaryStudentReducer,
  homeworkSummaryStudent: homeworkSummaryStudentReducer,
  homeworkStudent: homeworkStudentReducer,
  createTimeSheetStudent: createTimeSheetStudentReducer,
});

export default rootReducers;
