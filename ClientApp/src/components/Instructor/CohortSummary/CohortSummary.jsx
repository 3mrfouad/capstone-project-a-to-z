import React from "react";
import { Table, Container, Button } from "react-bootstrap";
import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { cohortSummaryInstructor } from "../../../actions/instructorActions";

const CourseSummaryInstructor = () => {
  const dispatch = useDispatch();
  const { products, loading } = useSelector(
    (state) => state.cohortSummaryInstructor
  );
  useEffect(() => {
    dispatch(cohortSummaryInstructor());
  }, [dispatch]);
  return (
    <React.Fragment>
      <Container>
        <Table>
          <thead>
            <tr>
              <th>Cohort Name</th>
              <th>Capacity</th>
              <th>Mode</th>
              <th>Start Date</th>
              <th>End Date</th>
              <th>City</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td>4.2</td>
              <td>20</td>
              <td>Remote</td>
              <td>2020-08-04</td>
              <td>2021-01-30</td>
              <td>Edmonton </td>
              <td>Edit | Retire</td>
            </tr>
            <tr>
              <td>4.2</td>
              <td>20</td>
              <td>Remote</td>
              <td>2020-08-04</td>
              <td>2021-01-30</td>
              <td>Edmonton </td>
              <td>Edit | Retire</td>
            </tr>
            <tr>
              <td>4.2</td>
              <td>20</td>
              <td>Remote</td>
              <td>2021-08-04</td>
              <td>2022-01-30</td>
              <td>Calgary</td>
              <td>Edit | Retire</td>
            </tr>
          </tbody>
        </Table>
        <Button>Back</Button> <Button>Register Student</Button>{" "}
        <Button>Create Cohort</Button>
      </Container>
    </React.Fragment>
  );
};

export default CourseSummaryInstructor;
