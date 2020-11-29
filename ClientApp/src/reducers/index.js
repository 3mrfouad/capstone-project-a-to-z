import { combineReducers } from "redux";
import {
  cohortSummaryInstructorReducer,
  cohortCreateReducer,
  cohortEditReducer,
  courseCreateReducer,
  courseEditReducer,
  homeworkSummaryInstructorReducer,
} from "./instructorReducer";
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
  cohortCreate: cohortCreateReducer,
  cohortEdit: cohortEditReducer,
  courseCreate: courseCreateReducer,
  courseEdit: courseEditReducer,
  homeworkSummaryInstructor: homeworkSummaryInstructorReducer,
});

export default rootReducers;
