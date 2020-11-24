import React from "react";
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";
import Header from "./components/shared/Header/Header";
import Login from "./screens/Login/Login";
import CourseSummaryStudent from "./components/Student/CourseSummaryStudent/CourseSummaryStudent";
import CourseSummaryInstructor from "./components/Instructor/CourseSummaryInstructor/CourseSummaryInstructor";
import HomeworkSummaryStudent from "./components/Student/HomeworkSummaryStudent/HomeworkSummaryStudent";
import HomeworkStudent from "./components/Student/HomeworkStudent/HomeworkStudent";
import SideBar from "./components/shared/SideBar/SideBar";
import StudentGrades from "./components/Student/StudentGrades/StudentGrades";
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
          <Route path="/studentgrades" exact component={StudentGrades} />
        </Switch>
      </Router>
    </React.Fragment>
  );
};

export default App;
