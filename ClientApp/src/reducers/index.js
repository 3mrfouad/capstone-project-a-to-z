import { combineReducers } from "redux";
import {
  cohortSummaryInstructorReducer,
  cohortCreateReducer,
  cohortEditReducer,
  courseCreateReducer,
  courseEditReducer,
  homeworkSummaryInstructorReducer,
  cohortGetStateReducer,
  manageCourseReducer,
} from "./instructorReducer";
import {
  courseSummaryStudentReducer,
  homeworkSummaryStudentReducer,
  homeworkStudentReducer,
  createTimeSheetStudentReducer,
  updateTimeSheetStudentReducer,
} from "./studentReducer";

const rootReducers = combineReducers({
  cohortSummaryInstructor: cohortSummaryInstructorReducer,
  courseSummaryStudent: courseSummaryStudentReducer,
  homeworkSummaryStudent: homeworkSummaryStudentReducer,
  homeworkStudent: homeworkStudentReducer,
  createTimeSheetStudent: createTimeSheetStudentReducer,
  updateTimeSheetStudent: updateTimeSheetStudentReducer,
  cohortCreate: cohortCreateReducer,
  cohortEdit: cohortEditReducer,
  courseCreate: courseCreateReducer,
  courseEdit: courseEditReducer,
  homeworkSummaryInstructor: homeworkSummaryInstructorReducer,
  cohortGetState: cohortGetStateReducer,
  manageCourse: manageCourseReducer,
});

export default rootReducers;
