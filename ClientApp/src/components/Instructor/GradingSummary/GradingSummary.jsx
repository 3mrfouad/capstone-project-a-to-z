import React, { useEffect } from "react";
import { Table, Container, Button, Nav, Row, Col } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import { getGradeSummaryInstructor } from "../../../actions/instructorActions";
import { getHomeworkSummaryInstructor } from "../../../actions/instructorActions";

import { Link } from "react-router-dom";

const GradingSummary = ({ match }) => {
  const cohortId = match.params.cohortId;
  const homeworkId = match.params.homeworkId;
  const courseId = match.params.courseId;

  const dispatch = useDispatch();
  useEffect(() => {
    // get cohort by id
    // populate the cohort data in here
    dispatch(getGradeSummaryInstructor({ cohortId, homeworkId }));
    dispatch(getHomeworkSummaryInstructor({ courseId, cohortId }));
  }, [dispatch]);

  const { homeworkSummary } = useSelector(
    (state) => state.homeworkSummaryInstructor
  );
  const { loading, error, grade } = useSelector(
    (state) => state.gradeSummaryInstructor
  );
  console.log("Loading: ", loading);
  console.log("Grades: ", grade);
  console.log("Loading: ", loading);
  console.log("Homeworks: ", homeworkSummary);

  while (
    homeworkSummary === undefined ||
    loading === undefined ||
    grade === undefined
  ) {
    return <h3>Loading ...</h3>;
  }

  return (
    <React.Fragment>
      {loading ? (
        <p></p>
      ) : (
        <Container>
          <Row>
            {
              <Col xs={2}>
                <Nav defaultActiveKey="/home" className="flex-column">
                  {homeworkSummary.map((homework, index) => (
                    <Nav.Link
                      href={`/gradingsummary/${cohortId}/${homework.homeworkId}/${courseId}`}
                      key={index}
                    >
                      {homework.title}
                    </Nav.Link>
                  ))}
                </Nav>
              </Col>
            }
            <Col xs={10}>
              <Table>
                <thead>
                  <tr>
                    <th>Student Name</th>
                    <th>Requirements Marks</th>
                    <th>Challenges Marks</th>
                    <th>Total Marks</th>
                    <th>Total Time</th>
                  </tr>
                </thead>
                <tbody>
                  {grade.map((Indvgrade, index) => (
                    <tr key={index}>
                      <td>{Indvgrade.studentName}</td>
                      <td>{Indvgrade.marksInRequirement}</td>
                      <td>{Indvgrade.marksInChallenge}</td>
                      <td>{Indvgrade.totalMarks}</td>
                      <td>{Indvgrade.totalTimeSpentOnHomework}</td>
                    </tr>
                  ))}
                </tbody>
              </Table>
              <Button>Back</Button>{" "}
              <Button className="float-right">Create</Button>
            </Col>
          </Row>
        </Container>
      )}
    </React.Fragment>
  );
};

export default GradingSummary;
