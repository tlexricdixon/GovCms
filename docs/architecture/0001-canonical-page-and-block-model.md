# ADR 0001: Canonical page and block model

Status: Accepted

## Context

The original GovCms page editor used small integer-keyed `Page` and `PageBlock`
entities. Piranha-derived models later introduced GUID identifiers, generic page
types, field wrappers, reflection-based registration, and a second persistence
model. That made the working admin flow depend on infrastructure it did not need.

## Decision

- `Manager.Models.Page` and `Manager.Models.PageBlock` are the canonical page
  persistence entities.
- Both inherit `BaseModel` and use integer identifiers.
- Only explicitly registered canonical entities are included in `LocalDbContext`.
- MVC input and display models are separate from EF entities and are mapped
  explicitly.
- The initial supported block types are Heading, Paragraph, and Link.
- Public routing returns only active, published pages.
- Link schemes are allowlisted when content is saved and checked again when it is
  rendered.
- Piranha-derived types are reference material. They do not determine database
  keys, EF relationships, controller contracts, or rendering behavior.

## Bringing useful Piranha blocks forward

Each useful Piranha block will be rewritten as a small GovCms block definition:

1. A stable type key owned by GovCms.
2. A typed input/data model containing only the fields that block requires.
3. Server-side validation with explicit length, URL, and media constraints.
4. An explicit editor partial.
5. An explicit renderer partial or view component.
6. Registration in a fixed allowlist through dependency injection.

Blocks will not be discovered by scanning assemblies, and persisted content will
never contain CLR type names. Arbitrary HTML, scripts, styles, and unregistered
block types are rejected.

The current three blocks use dedicated columns to restore the proven vertical
slice. Before adding a large block catalog, we will choose either typed JSON
payloads with a strict serializer/validator registry or dedicated detail tables.
That choice will be made from actual block requirements rather than inherited
Piranha infrastructure.

## Consequences

- Page creation and basic blocks stay simple and auditable.
- Piranha features can be adopted one at a time without importing its manager or
  persistence architecture.
- Adding a block requires explicit code in the input, validation, persistence,
  and rendering paths, which is intentional for a public government site.
