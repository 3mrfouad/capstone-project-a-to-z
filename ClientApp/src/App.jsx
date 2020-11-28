import React from "react";
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";
import Header from "./components/shared/Header/Header";
import Login from "./screens/Login/Login";
import CourseSummaryStudent from "./components/Student/CourseSummaryStudent/CourseSummaryStudent";
import CourseSummaryInstructor from "./components/Instructor/CourseSummaryInstructor/CourseSummaryInstructor";
import HomeworkSummaryStudent from "./components/Student/HomeworkSummaryStudent/HomeworkSummaryStudent";
import HomeworkStudent from "./components/Student/HomeworkStudent/HomeworkStudent";
import SideBar from "./components/shared/SideBar/SideBar";
import GradesStudent from "./components/Student/GradesStudent/GradesStudent";
import CohortCard from "./components/Instructor/CohortCard/CohortCard";
import CohortSummary from "./components/Instructor/CohortSummary/CohortSummary";
import GradingSummary from "./components/Instructor/GradingSummary/GradingSummary";
import CourseCreateEdit from "./components/Instructor/CourseCreateEdit/CourseCreateEdit";
import GradesInstructorPage from "./screens/GradesInstructorPage/GradesInstructorPage";

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
          <Route path="/sidebar" exact component={SideBar} />
          <Route path="/gradesstudent" exact component={GradesStudent} />
          <Route path="/cohortcard" exact component={CohortCard} />
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
        </Switch>
      </Router>
    </React.Fragment>
  );
};

export default App;
