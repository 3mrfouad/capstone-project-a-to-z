import React from "react";
import BootstrapTable from "react-bootstrap-table-next";
import cellEditFactory from "react-bootstrap-table2-editor";

const dummyData = [
  {
    rubricId: 115,
    studentId: 7,
    mark: 1,
    instructorComment: null,
    studentComment: null,
    archive: true,
    rubric: {
      rubricId: 115,
      homeworkId: 1,
      isChallenge: false,
      criteria: "Practice submitted on due date to GitHub classroom",
      weight: 1,
      archive: true,
      homework: {
        homeworkId: 1,
        courseId: 1,
        cohortId: 3,
        instructorId: 3,
        isAssignment: false,
        title: "Hello World, Hello User, Hello Everybody",
        avgCompletionTime: 8.0,
        dueDate: "2020-08-10T09:00:00",
        releaseDate: "2020-07-27T15:00:00",
        documentLink:
          "https://drive.google.com/file/d/1HMcsRK8GZcZRZM_vUtHfzhMCDjuz0mTD/view?usp=sharing",
        gitHubClassRoomLink: null,
        archive: true,
        course: null,
        cohort: null,
        instructor: null,
        shoutOuts: [],
        timesheets: [],
        rubrics: [],
      },
      grades: [],
    },
    student: null,
  },
];

const GradesInstructor = () => {
  return (
    <React.Fragment>
      {/* <BootstrapTable
        keyField="id"
        data={products}
        columns={columns}
        cellEdit={cellEditFactory({
          mode: "click",
          blurToSave: true,
        })}
      /> */}
    </React.Fragment>
  );
};

export default GradesInstructor;
