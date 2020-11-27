import React from "react";
import { Table, Container } from "react-bootstrap";

const ManageCourseInstructor = () => {
  return (
    <React.Fragment>
      <Container>
        <h2>Cohort 4.2</h2>
        <Table>
          <thead>
            <tr>
              <th>Course Name</th>
              <th>Description</th>
              <th>Duration</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td>React.js</td>
              <td>React Basics</td>
              <td>10</td>
              <td>Edit | Archive</td>
            </tr>
            <tr>
              <td>CSS</td>
              <td>CSS Basics</td>
              <td>10</td>
              <td>Edit | Archive</td>
            </tr>
            <tr>
              <td>HTML</td>
              <td>HTML Basics</td>
              <td>10</td>
              <td>Edit | Archive</td>
            </tr>
          </tbody>
        </Table>
        <button type="button" className="btn btn-link">
          Back
        </button>{" "}
        <Button className="float-right mr-3">Create Course</Button>
      </Container>
    </React.Fragment>
  );
};

export default ManageCourseInstructor;
