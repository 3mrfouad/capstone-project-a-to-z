import React from "react";
import { Table, Container, Row, Col, Nav } from "react-bootstrap";
import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { homeworkSummaryStudent } from "../../../actions/studentActions";

const HomeworkSummaryStudent = () => {
  const dispatch = useDispatch();
  const { homework, loading } = useSelector(
    (state) => state.homeworkSummaryStudent
  );
  useEffect(() => {
    dispatch(homeworkSummaryStudent());
  }, [dispatch]);
  return (
    <React.Fragment>
      <Container>
        <Row>
          <Col xs={2}>
            <Nav defaultActiveKey="/home" className="flex-column">
              <Nav.Link href="/home">Course Name</Nav.Link>
              <Nav.Link eventKey="link-1">Course Name</Nav.Link>
              <Nav.Link eventKey="link-2">Course Name</Nav.Link>
              <Nav.Link eventKey="link-2">Course Name</Nav.Link>
              <Nav.Link eventKey="link-2">Course Name</Nav.Link>
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
                <tr>
                  <td>React101</td>
                  <td>2020-11-19</td>
                  <td>
                    {" "}
                    <a href="#">GitHubLink</a>{" "}
                  </td>
                  <td>Practice</td>
                  <td>View | Grades</td>
                </tr>
                <tr>
                  <td>React102</td>
                  <td>2020-11-29</td>
                  <td>
                    <a href="#">GitHubLink</a>
                  </td>
                  <td>Assignment</td>
                  <td>View | Grades</td>
                </tr>
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
