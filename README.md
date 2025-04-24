### Problems in original Speaker class

- Violated Single Responsibility Principle
- Tightly coupled logic with no abstractions
- Huge method doing too many things
- Comments trying to explain bad code
- Hardcoded logic without context
- Exceptions not used meaningfully

### Clean Code and SOLID improvements

- Broke logic into smaller private methods
- Introduced custom exceptions
- Encapsulated logic in `Speaker` domain model
- Used interface for repository (Dependency Inversion)
- Extract Method
- Use of Switch Expression
- Encapsulation
- Simple dependency injection (via constructor)
