import React from "react";
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";
import Header from "./components/shared/Header/Header";
import Login from "./screens/Login/Login";
import CourseSummaryStudent from "./components/Student/CourseSummaryStudent/CourseSummaryStudent";
import CourseSummaryInstructor from "./components/Instructor/CourseSummaryInstructor/CourseSummaryInstructor";
import HomeworkSummaryStudent from "./components/Student/HomeworkSummaryStudent/HomeworkSummaryStudent";
import HomeworkStudent from "./components/Student/HomeworkStudent/HomeworkStudent";
import GradesStudent from "./components/Student/GradesStudent/GradesStudent";
import CohortCreate from "./components/Instructor/CohortCreate/CohortCreate";
import CohortEdit from "./components/Instructor/CohortEdit/CohortEdit";
import CohortSummary from "./components/Instructor/CohortSummary/CohortSummary";
import GradingSummary from "./components/Instructor/GradingSummary/GradingSummary";
import CourseCreateEdit from "./components/Instructor/CourseCreate/CourseCreate";
import GradesInstructorPage from "./screens/GradesInstructorPage/GradesInstructorPage";
import ManageCourse from "./components/Instructor/ManageCourses/ManageCourses";

const App = () => {
  return (
    <React.Fragment>
      <Router>
        <Header />
        <Switch>
          <Route path="/" exact component={Login} />
          <Route path="/student" exact component={CourseSummaryStudent} />
          <Route path="/instructor" exact component={CourseSummaryInstructor} />
          <Route
            path="/studenthomework"
            exact
            component={HomeworkSummaryStudent}
          />
          <Route
            path="/homeworkcardstudent"
            exact
            component={HomeworkStudent}
          />
          <Route path="/gradesstudent" exact component={GradesStudent} />
          <Route path="/cohortcreate" exact component={CohortCreate} />
          <Route path="/cohortedit/:id" exact component={CohortEdit} />
          <Route path="/cohortsummary" exact component={CohortSummary} />
          <Route path="/gradingsummary" exact component={GradingSummary} />
          <Route path="/coursecreate" exact component={CourseCreateEdit} />
          <Route
            path="/GradesInstructorPage"
            exact
            component={GradesInstructorPage}
          />
          <Route
            path="/CourseSummaryStudent"
            exact
            component={CourseSummaryStudent}
          />
          <Route path="/managecourse" exact component={ManageCourse} />
        </Switch>
      </Router>
    </React.Fragment>
  );
};

export default App;
