import React, { useEffect } from "react";
import { Table, Container, Button, Nav, Row, Col } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import { getGradeSummaryInstructor } from "../../../actions/instructorActions";
import { getHomeworkSummaryInstructor } from "../../../actions/instructorActions";
import { LinkContainer } from "react-router-bootstrap";

import Loader from "../../shared/Loader/Loader";

const GradingSummary = ({ match, history }) => {
  const cohortId = match.params.cohortId;
  const homeworkId = match.params.homeworkId;
  const courseId = match.params.courseId;

  const dispatch = useDispatch();
  useEffect(() => {
    dispatch(getGradeSummaryInstructor({ cohortId, homeworkId }));
    dispatch(getHomeworkSummaryInstructor({ courseId, cohortId }));
  }, [dispatch, homeworkId]);
  const goBack = () => {
    history.goBack();
  };
  const { homeworkSummary } = useSelector(
    (state) => state.homeworkSummaryInstructor
  );
  const { loading, error, grade } = useSelector(
    (state) => state.gradeSummaryInstructor
  );

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
        <Loader />
      ) : (
        <Container>
          <Row>
            {
              <Col xs={2}>
                <Nav className="flex-column">
                  {homeworkSummary.map((homework, index) => (
                    <LinkContainer
                      to={`/gradingsummary/${cohortId}/${homework.homeworkId}/${courseId}`}
                    >
                      <Nav.Link key={index}>{homework.title}</Nav.Link>
                    </LinkContainer>
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
              <button type="button" className="btn btn-link" onClick={goBack}>
                Back
              </button>
            </Col>
          </Row>
        </Container>
      )}
    </React.Fragment>
  );
};

export default GradingSummary;
