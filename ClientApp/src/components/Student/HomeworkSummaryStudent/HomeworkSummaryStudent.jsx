import React from "react";
import { Table, Container, Row, Col, Nav } from "react-bootstrap";
import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import {
  homeworkSummaryStudent,
  courseSummaryStudent,
} from "../../../actions/studentActions";

const HomeworkSummaryStudent = () => {
  const dispatch = useDispatch();
  const { homework, loading } = useSelector(
    (state) => state.homeworkSummaryStudent
  );
  const { courses } = useSelector((state) => state.courseSummaryStudent);
  useEffect(() => {
    dispatch(homeworkSummaryStudent());
    dispatch(courseSummaryStudent());
  }, [dispatch]);
  return (
    <React.Fragment>
      <Container>
        <Row>
          <Col xs={2}>
            <Nav defaultActiveKey="/home" className="flex-column mt-5">
              {courses.map((course, index) => (
                <Nav.Link key={index} eventKey="link-1">
                  {" "}
                  {course.name}{" "}
                </Nav.Link>
              ))}
              {/* <Nav.Link href="/home">Course Name</Nav.Link>
              <Nav.Link eventKey="link-1">Course Name</Nav.Link> */}
            </Nav>
          </Col>
          <Col xs={10}>
            <Table>
              <thead>
                <tr>
                  <th>Homework Name</th>
                  <th>Due Date</th>
                  <th>GitHub</th>
                  <th>Category</th>
                  <th>Details</th>
                </tr>
              </thead>
              <tbody>
                {homework.map((item, index) => (
                  <tr key={index}>
                    <td>{item.homeworkId}</td>
                    <td>{item.dueDate}</td>
                    <td>{item.gitHubClassRoomLink}</td>
                    <td>category</td>
                    <td>
                      <a href="#">View</a>|<a href="#">Grades</a>
                    </td>
                  </tr>
                ))}
              </tbody>
            </Table>
            <button type="button" className="btn btn-link">
              Back
            </button>
          </Col>
        </Row>
      </Container>
    </React.Fragment>
  );
};

export default HomeworkSummaryStudent;
