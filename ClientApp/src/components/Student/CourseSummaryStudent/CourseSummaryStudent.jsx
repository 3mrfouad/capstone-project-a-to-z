import React from "react";
import { Table, Container, Button } from "react-bootstrap";
import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { courseSummaryStudent } from "../../../actions/studentActions";

const CourseSummaryStudent = () => {
  const dispatch = useDispatch();
  const { courses, loading } = useSelector(
    (state) => state.courseSummaryStudent
  );
  useEffect(() => {
    dispatch(courseSummaryStudent());
  }, [dispatch]);
  return (
    <React.Fragment>
      <Container>
        <Table>
          <thead>
            <tr>
              <th>Course Name</th>
              <th>Description</th>
              <th>Duration</th>
              <th>Instructor</th>
              <th>Homework</th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td>React.js</td>
              <td>
                React Basicsfffffffffffffffffffffffffffffffffffffffffffffffffff
              </td>
              <td>10</td>
              <td>Instructor A</td>
              <td>
                <a href="#">Homework</a>{" "}
              </td>
            </tr>
            <tr>
              <td>CSS</td>
              <td>CSS Basics</td>
              <td>10</td>
              <td>Instructor B</td>
              <td>
                <a href="#">Homework</a>{" "}
              </td>
            </tr>
            <tr>
              <td>HTML</td>
              <td>HTML Basics</td>
              <td>10</td>
              <td>Instructor B</td>
              <td>
                <a href="#">Homework</a>{" "}
              </td>
            </tr>
          </tbody>
        </Table>
        <button type="button" className="btn btn-link">
          Back
        </button>
      </Container>
    </React.Fragment>
  );
};

export default CourseSummaryStudent;
